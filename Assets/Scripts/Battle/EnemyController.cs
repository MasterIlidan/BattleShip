using DefaultNamespace;
using UnityEngine;

namespace Battle
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] public GameObject shipPrefab;


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
            
        }


    }
}