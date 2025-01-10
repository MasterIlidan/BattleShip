using System.Collections.Generic;
using Battle;
using Placement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using StateMachine = DefaultNamespace.StateMachine;

public class GameManager : MonoBehaviour
{
    private StateMachine _stateMachine;
    public UnityEvent<GameState> onStateChanged;
    public UnityEvent<Vector3> onCameraPosChanged;
        
    BattleController.Winner _winner;
    //public Dictionary<string, Ship> playerShips; 
    //public DefaultNamespace.Statee State = DefaultNamespace.Statee.Start;
    void Start()
    {
        //TODO: временно PLACEMENT, должно быть MAINMENU
        OnStateSwitch(GameState.Placement);
        GameObject
            .FindGameObjectWithTag("Placement Controller")
            .GetComponent<PlacementScript>()
            .battleStartEvent.AddListener(OnPrepareBattle);
        GameObject
            .FindGameObjectWithTag("Battle Controller")
            .GetComponent<BattleController>()
            .onGameOverEvent.AddListener(OnGameOver);
    }

        
        
    public void OnStateSwitch(GameState newState)
    {
        _stateMachine.CurrentState = newState;
        switch (_stateMachine.CurrentState)
        {
            case GameState.Mainmenu:
            {
                print("Main Menu");
                onCameraPosChanged.Invoke(new Vector3(-58f, 0f, -10f));
                onStateChanged?.Invoke(GameState.Mainmenu);
                break;
            }
            case GameState.Placement:
            {
                print("Placement");
                onCameraPosChanged?.Invoke(new Vector3(-29f, 0f, -10f));
                onStateChanged?.Invoke(GameState.Placement);
                break;
            }
            case GameState.Battle:
            {
                print("Battle");
                onCameraPosChanged?.Invoke(new Vector3(0f, 0f, -10f)); 
                onStateChanged?.Invoke(GameState.Battle);
                break;
            }
            case GameState.Endgame:
            {
                onCameraPosChanged?.Invoke(new Vector3(29f, 0f, -10f));
                onStateChanged?.Invoke(GameState.Endgame);
                print("End Game");
                break;
            }
            default:
            {
                throw new UnexpectedEnumValueException<GameState>(newState);
            }
        }
    }

    public void OnPrepareBattle(List<Ship> ships)
    {
        print("Prepare Battle");
        //playerShips = ships;
        //TODO: временное копирование всего расположения кораблей игрока как 
            
        OnStateSwitch(GameState.Battle);
    }

    private void OnGameOver(BattleController.Winner winner)
    {
        print("Game Over");
        OnStateSwitch(GameState.Endgame);
    }
}