using System;
using System.Collections.Generic;
using DefaultNamespace;
using Placement;
using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    public class Winner
    {
        public bool isPlayerWinner { get; }

        public Winner(bool isPlayerWinner)
        {
            this.isPlayerWinner = isPlayerWinner;
        }
    }

    List<Ship> playerShips = new();
    List<Ship> enemyShips = new();
    public GameManager gameManager;

    public UnityEvent<bool> OnChangeTurn;
    public UnityEvent<Ship> OnShipDestroyedEvent;
    public UnityEvent<Winner> OnGameOverEvent;

    bool isPlayerTurn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged
            .AddListener(OnStateChanged);
        GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().OnTileClickedEvent
            .AddListener(OnTileClicked);
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged
            .RemoveListener(OnStateChanged);
        GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().OnTileClickedEvent
            .AddListener(OnTileClicked);
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
        GameObject playerShipsCollection = GameObject.Find("PlayerShips");
        for (int i = 0; i < playerShipsCollection.transform.childCount; i++)
        {
            var ship = playerShipsCollection.transform.GetChild(i).gameObject.GetComponent<Ship>();
            playerShips.Add(ship);
            ship.OnShipDestroyed.AddListener(OnShipDestroyed);
            ship.OnShipDamaged.AddListener(OnShipDamaged);
        }

        GameObject enemyShipsCollection = GameObject.Find("EnemyShips");
        for (int i = 0; i < enemyShipsCollection.transform.childCount; i++)
        {
            var ship = enemyShipsCollection.transform.GetChild(i).gameObject.GetComponent<Ship>();
            enemyShips.Add(ship);
            ship.OnShipDestroyed.AddListener(OnShipDestroyed);
            ship.OnShipDamaged.AddListener(OnShipDamaged);
        }

        OnChangeTurn.Invoke(true);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged
            .AddListener(OnStateChanged);
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if (isPlayerTurn)
        {
            transform.position = new Vector3(4, 8, 0);
        }
        else
        {
            transform.position = new Vector3(0, 8, 0);
        }
        OnChangeTurn.Invoke(isPlayerTurn);
    }

    private void OnShipDestroyed(Ship ship)
    {
        if (isPlayerTurn && ship.isPlayerShip)
        {
            Debug.LogWarning("player turn and player ship destroyed...");
        }

        if (ship.isPlayerShip)
        {
            playerShips.Remove(ship);
            print("player ship destroyed, removed from list");
            if (playerShips.Count == 0)
            {
                print("player defeated!");
                OnGameOverEvent.Invoke(new Winner(false));
                return;
            }
            OnChangeTurn.Invoke(false);
        }
        else
        {
            enemyShips.Remove(ship);
            print("enemy ship destroyed, removed from list");
            if (enemyShips.Count == 0)
            {
                print("enemy defeated!");
                OnGameOverEvent.Invoke(new Winner(true));
            }
            OnChangeTurn.Invoke(true);
        }

        {
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "EnemyTurn")
        {
            OnChangeTurn.Invoke(false);
            return;
        }

        if (other.name == "PlayerTurn")
        {
            OnChangeTurn.Invoke(true);
            return;
        }
    }

    private void OnShipDamaged(Ship ship)
    {
        if (isPlayerTurn && ship.isPlayerShip)
        {
            Debug.LogWarning("playerturn and playership damaged...");
        }
    }
}