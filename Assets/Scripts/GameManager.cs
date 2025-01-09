using System;
using System.Collections.Generic;
using Placement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        public StateMachine _stateMachine;
        public UnityEvent<GameState> OnStateChanged;
        public UnityEvent<Vector3> OnCameraPosChanged;
        
        //public Dictionary<string, Ship> playerShips; 
        //public DefaultNamespace.Statee State = DefaultNamespace.Statee.Start;
        void Start()
        {
            //TODO: временно PLACEMENT, должно быть MAINMENU
            OnStateSwitch(GameState.PLACEMENT);
            GameObject
                .FindGameObjectWithTag("Placement Controller")
                .GetComponent<PlacementScript>()
                .BattleStartEvent.AddListener(OnPrepareBattle);
        }

        
        
        public void OnStateSwitch(GameState newState)
        {
            _stateMachine.CurrentState = newState;
            switch (_stateMachine.CurrentState)
            {
                case GameState.MAINMENU:
                {
                    print("Main Menu");
                    //OnCameraPosChanged.Invoke(cameraPosition);
                    OnStateChanged?.Invoke(GameState.MAINMENU);
                    break;
                }
                case GameState.PLACEMENT:
                {
                    print("Placement");
                    OnCameraPosChanged?.Invoke(new Vector3(-29f, 0f, -10f));
                    OnStateChanged?.Invoke(GameState.PLACEMENT);
                    break;
                }
                case GameState.BATTLE:
                {
                    print("Battle");
                    OnCameraPosChanged?.Invoke(new Vector3(0f, 0f, -10f)); OnStateChanged?.Invoke(GameState.BATTLE);
                    break;
                }
                case GameState.ENDGAME:
                {
                    OnStateChanged?.Invoke(GameState.ENDGAME);
                    print("End Game");
                    break;
                }
                default:
                {
                    throw new UnexpectedEnumValueException<GameState>(newState);
                }
            }
        }

        public void OnPrepareBattle(Dictionary <string, Ship> ships)
        {
            print("Prepare Battle");
            //playerShips = ships;
            OnStateSwitch(GameState.BATTLE);
        }
    }

    
}