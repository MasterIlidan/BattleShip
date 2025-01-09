using System;
using System.Collections.Generic;
using DefaultNamespace;
using Placement;
using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    Dictionary<string, Ship> playerShips = new ();
    Dictionary<string, Ship> enemyShips = new ();
    public GameManager gameManager;
    
    public UnityEvent<bool> OnChangeTurn;
    
    
    bool isPlayerTurn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.AddListener(OnStateChanged);
        GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().OnTileClickedEvent.AddListener(OnTileClicked);
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.RemoveListener(OnStateChanged);
        GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().OnTileClickedEvent.AddListener(OnTileClicked);
    }
//Если кликнуто поле, значит мимо
    void OnTileClicked(GameObject tile)
    {
        print("OnTileClicked BattleController");
        if (!isPlayerTurn)
        {
            print("not player turn");
            return;
        }
        tile.GetComponent<BoxCollider2D>().enabled = false;
        tile.GetComponent<SpriteRenderer>().enabled = true;
        ChangeTurn();
    }

    void OnStateChanged(GameState newState)
    {
        if (newState != GameState.BATTLE)
        {
            return;
        }
        
        print("Start Battle");
        OnSetupBattleField();
    }

    void OnSetupBattleField()
    {
        playerShips = GameObject.FindGameObjectWithTag("Placement Controller").GetComponent<PlacementScript>().PlacedShips;
        //TODO: временно корабли противника имеют точно такое же расположение
        //enemyShips = playerShips;
        foreach (var ship in playerShips.Values)
        {
            ship.OnShipDestroyed.AddListener(OnShipDestroyed);
            ship.OnShipDamaged.AddListener(OnShipDamaged);
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.AddListener(OnStateChanged);
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        OnChangeTurn.Invoke(isPlayerTurn);
    }

    private void OnShipDestroyed(Ship ship)
    {
        
    }

    private void OnShipDamaged()
    {
        
    }
}
