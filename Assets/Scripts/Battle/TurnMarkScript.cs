using UnityEngine;

namespace Battle
{
    public class TurnMarkScript : MonoBehaviour
    {
        private readonly Vector2[] _markPos =
        {
            new Vector2(-4, 3.88f),
            new Vector2(4, 3.88f)
        };

        void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Battle Controller").GetComponent<BattleController>().onChangeTurn.AddListener(OnChangePlayerTurn);
        }

        void OnDisable()
        {
            GameObject.FindGameObjectWithTag("Battle Controller").GetComponent<BattleController>().onChangeTurn.RemoveListener(OnChangePlayerTurn);
        }

        public void OnChangePlayerTurn(bool isPlayerTurn)
        {
            int turn = isPlayerTurn ? (int)MarkPosition.Player : (int)MarkPosition.Enemy;
            transform.position = _markPos[turn];
        }

        enum MarkPosition
        {
            Enemy = 0, Player = 1 
        }
    
    }
}
