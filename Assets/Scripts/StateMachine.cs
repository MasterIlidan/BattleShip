using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateMachine : MonoBehaviour
    {
        public GameState CurrentState { get;  set; }
        
        
        

    }
}

public enum GameState
{
    MAINMENU,
    PLACEMENT,
    BATTLE,
    ENDGAME
}