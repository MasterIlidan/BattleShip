using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Battle
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] public GameObject shipPrefab;
        private List<GameObject> playerShips;
        public float hitChance = 0.5f;
        public GameObject currentTarget;

        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Battle Controller")
                .GetComponent<BattleController>().OnChangeTurn.AddListener(OnEnemyTurn);
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().OnStateChanged.AddListener(OnStateChanged);
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().OnStateChanged.RemoveListener(OnStateChanged);
        }

        private void OnStateChanged(GameState state)
        {
            if (state != GameState.BATTLE) return;
            GameObject playerShipsCollectionGameObject
                = GameObject.Find("PlayerShips");
            List<GameObject> playerShipsCollection = new();
            for (int i = 0; i < playerShipsCollectionGameObject.transform.childCount; i++)
            {
                playerShipsCollection.Add(playerShipsCollectionGameObject.transform.GetChild(i).gameObject);
            }
        }

        private void OnEnemyTurn(bool isPlayerTurn)
        {
            if (isPlayerTurn) return;
            if (currentTarget != null)
            {
                MakeDamage(currentTarget);
            } 

            float isHit = UnityEngine.Random.Range(0f, 1f);
            if (isHit > hitChance)
            {
                currentTarget = ChooseTarget();
                MakeDamage(currentTarget);
                return;
            }
            print("Enemy missing");
            
        }

        private void MakeDamage(GameObject o)
        {
            for (int i = 0; i < o.transform.childCount; i++)
            {
                GameObject obj = o.transform.GetChild(i).gameObject;
                ShipHitScript shipHitScript = obj.GetComponent<ShipHitScript>();
                if (obj.name == "DamagedSprite" &&
                    shipHitScript != null)
                {
                    shipHitScript.OnShipDamage();
                    return;
                }
            }
        }

        private GameObject ChooseTarget()
        {
            int randomIndex = UnityEngine.Random.Range(0, playerShips.Count);
            GameObject playerShip = playerShips[randomIndex];
            return playerShip;
        }
    }
}