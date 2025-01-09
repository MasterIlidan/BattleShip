using DefaultNamespace;
using UnityEngine;

namespace Battle
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] public GameObject shipPrefab;
        private readonly Vector3 offset = new Vector3(7.996694f, 0f, 0f);

        private void OnEnable()
        {
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

            GameObject playerShips = GameObject.FindGameObjectWithTag("Player Ships");
            GameObject enemyShips = GameObject.FindGameObjectWithTag("Enemy Ships");

            for (int i = 0; i < playerShips.transform.childCount; i++)
            {
                GameObject enemyShip = Instantiate(playerShips.transform.GetChild(i).gameObject,
                    playerShips.transform.GetChild(i).position - offset,
                    Quaternion.identity);
                print("copy player ship " + enemyShip.name + " to enemy field");
                enemyShip.transform.SetParent(enemyShips.transform);
            }
            enemyShips.transform.rotation = new Quaternion(0f, 0f, 90f, 0f);
        }
    }
}