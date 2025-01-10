using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class EnemyController : MonoBehaviour
    {
        private List<GameObject> _playerShips;
        public float hitChance = 0.90f;
        public GameObject currentTarget;
        private readonly List<GameObject> _tiles = new();
        public GameObject rootTileObjectOfPlayerField;


        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Battle Controller")
                .GetComponent<BattleController>().onChangeTurn.AddListener(OnEnemyTurn);
            GameObject.FindGameObjectWithTag("Battle Controller")
                .GetComponent<BattleController>().onShipDestroyedEvent.AddListener(OnPlayerShipDestroyed);
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().onStateChanged.AddListener(OnStateChanged);
            
        }

        private void OnPlayerShipDestroyed(Ship playerShip)
        {
            currentTarget = null;
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().onStateChanged.RemoveListener(OnStateChanged);
        }

        private void OnStateChanged(GameState state)
        {
            if (state != GameState.Battle) return;
            GameObject playerShipsCollectionGameObject
                = GameObject.Find("PlayerShips");
            List<GameObject> playerShipsCollection = new();
            //собрать все корабли с поля игрока
            for (int i = 0; i < playerShipsCollectionGameObject.transform.childCount; i++)
            {
                playerShipsCollection.Add(playerShipsCollectionGameObject.transform.GetChild(i).gameObject);
            }

            _playerShips = playerShipsCollection;
            //собрать все плитки с поля игрока
            for (int i = 0; i < rootTileObjectOfPlayerField.transform.childCount; i++)
            {
                _tiles.Add(rootTileObjectOfPlayerField.transform.GetChild(i).gameObject);
            }


        }

        void OnEnemyTurn(bool isPlayerTurn)
        {
            print("Coroutine trying to make turn");
            if (isPlayerTurn) return;
            StartCoroutine(WaitForPlayerTurn());
            print("Hello from coroutine!");
            if (currentTarget != null)
            {
                MakeDamage(currentTarget);
                return;
            }
            
            float isHit = Random.Range(0f, 1f);
            if (isHit > hitChance)
            {
                currentTarget = ChooseTarget();
                MakeDamage(currentTarget);
                return;
            }

            print("Enemy missing");
            GameObject tileShotGameObject;
            do
            {
                var tileShot = Random.Range(0, _tiles.Count);
                tileShotGameObject = _tiles[tileShot];
                if (!tileShotGameObject.GetComponent<SpriteRenderer>().enabled
                    && tileShotGameObject.GetComponent<BoxCollider2D>().enabled) continue;
                Debug.LogWarning("Tile " + tileShotGameObject.name + " disabled or already shot?");
                _tiles.Remove(tileShotGameObject);
                tileShotGameObject = null;
            } while (tileShotGameObject is null);
            
            print("Mark " + tileShotGameObject.name + " as miss");
            tileShotGameObject.GetComponent<SpriteRenderer>().enabled = true;
            tileShotGameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindGameObjectWithTag("Battle Controller")
                .GetComponent<BattleController>().ChangeTurn();
        }

        IEnumerator WaitForPlayerTurn()
        {
            yield return new WaitForSeconds(1f);
        }


        void MakeDamage(GameObject o)
        {
            for (int i = 0; i < o.transform.childCount; i++)
            {
                GameObject obj = o.transform.GetChild(i).gameObject;
                ShipHitScript shipHitScript = obj.GetComponent<ShipHitScript>();
                obj.TryGetComponent(out BoxCollider2D boxCollider);
                if (boxCollider == null) continue;
                if (obj.name == "DamagedSprite" &
                    shipHitScript != null &
                    boxCollider.enabled
                   )
                {
                    shipHitScript.OnShipDamage();
                    return;
                }
            }

            _playerShips.Remove(o);
            currentTarget = null;
        }

        private GameObject ChooseTarget()
        {
            int randomIndex = Random.Range(0, _playerShips.Count);
            GameObject playerShip = _playerShips[randomIndex];
            return playerShip;
        }
    }
}