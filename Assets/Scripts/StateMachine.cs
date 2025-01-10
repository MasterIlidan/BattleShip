using UnityEngine;

namespace DefaultNamespace
{
    public class StateMachine : MonoBehaviour
    {
        public GameState CurrentState { get; set; }
    }
}

public enum GameState
{
    Mainmenu,
    Placement,
    Battle,
    Endgame
}