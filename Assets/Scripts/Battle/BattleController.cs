using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class BattleController : MonoBehaviour
    {
        public class Winner
        {
            public bool IsPlayerWinner { get; }

            public Winner(bool isPlayerWinner)
            {
                this.IsPlayerWinner = isPlayerWinner;
            }
        }

        private readonly List<Ship> _playerShips = new();
        private readonly List<Ship> _enemyShips = new();
        public GameManager gameManager;

        public UnityEvent<bool> onChangeTurn;
        public UnityEvent<Ship> onShipDestroyedEvent;
        public UnityEvent<Winner> onGameOverEvent;

        private bool _isPlayerTurn = true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().onStateChanged
                .AddListener(OnStateChanged);
            GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().onTileClickedEvent
                .AddListener(OnTileClicked);
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().onStateChanged
                .RemoveListener(OnStateChanged);
            GameObject.FindGameObjectWithTag("Tile click handler").GetComponent<TileClickHandler>().onTileClickedEvent
                .AddListener(OnTileClicked);
        }

//Если кликнуто поле, значит мимо
        void OnTileClicked(GameObject tile)
        {
            print("OnTileClicked BattleController");
            if (!_isPlayerTurn)
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
            if (newState != GameState.Battle)
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
                _playerShips.Add(ship);
                ship.onShipDestroyed.AddListener(OnShipDestroyed);
                ship.onShipDamaged.AddListener(OnShipDamaged);
            }

            GameObject enemyShipsCollection = GameObject.Find("EnemyShips");
            for (int i = 0; i < enemyShipsCollection.transform.childCount; i++)
            {
                var ship = enemyShipsCollection.transform.GetChild(i).gameObject.GetComponent<Ship>();
                _enemyShips.Add(ship);
                ship.onShipDestroyed.AddListener(OnShipDestroyed);
                ship.onShipDamaged.AddListener(OnShipDamaged);
            }

            onChangeTurn.Invoke(true);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().onStateChanged
                .AddListener(OnStateChanged);
        }

        public void ChangeTurn()
        {
            _isPlayerTurn = !_isPlayerTurn;
            if (_isPlayerTurn)
            {
                transform.position = new Vector3(4, 8, 0);
            }
            else
            {
                transform.position = new Vector3(0, 8, 0);
            }
            onChangeTurn.Invoke(_isPlayerTurn);
        }

        private void OnShipDestroyed(Ship ship)
        {
            if (_isPlayerTurn && ship.IsPlayerShip)
            {
                Debug.LogWarning("player turn and player ship destroyed...");
            }

            if (ship.IsPlayerShip)
            {
                _playerShips.Remove(ship);
                print("player ship destroyed, removed from list");
                if (_playerShips.Count == 0)
                {
                    print("player defeated!");
                    onGameOverEvent.Invoke(new Winner(false));
                    return;
                }
                onChangeTurn.Invoke(false);
            }
            else
            {
                _enemyShips.Remove(ship);
                print("enemy ship destroyed, removed from list");
                if (_enemyShips.Count == 0)
                {
                    print("enemy defeated!");
                    onGameOverEvent.Invoke(new Winner(true));
                }
                onChangeTurn.Invoke(true);
            }

            {
            }
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.name == "EnemyTurn")
            {
                onChangeTurn.Invoke(false);
                return;
            }

            if (other.name == "PlayerTurn")
            {
                onChangeTurn.Invoke(true);
            }
        }

        private void OnShipDamaged(Ship ship)
        {
            if (_isPlayerTurn && ship.IsPlayerShip)
            {
                Debug.LogWarning("playerturn and playership damaged...");
            }
        }
    }
}