using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Placement
{
    public class PlacementScript : MonoBehaviour
    {
        public Dictionary<string, Ship> PlacedShips = new();

        //public BattleStartEvent BattleStartEvent;
        public UnityEvent<Dictionary<string, Ship>> BattleStartEvent;
        [SerializeField] public GameObject shipPrefab;

        public void OnEnable()
        {
            BattleStartEvent.AddListener(
                GameObject
                    .FindGameObjectWithTag("GameController")
                    .GetComponent<GameManager>()
                    .OnPrepareBattle);
        }

        public void OnClickStartGame()
        {
            PlacedShips.Clear();
            OnPrepareGameStart();
            BattleStartEvent.Invoke(PlacedShips);
        }

        void OnPrepareGameStart()
        {
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
            foreach (var ship in ships)
            {
                var shipSize = ship.GetComponent<CaterScript>().ShipSize;
                var tiles = ship.GetComponent<CaterScript>().shipTilesList;
                var shipName = ship.name;
                if (tiles.Count != shipSize)
                    throw new ArgumentException("Ship size mismatch in ship: " + ship.name + " Value: "
                                                + tiles.Count + " Expected: "
                                                + shipSize);

                /*switch (shipSize)
                {
                    case 1:
                    {
                        shipName = "One";
                        break;
                    }
                    case 2:
                    {
                        shipName = "Double";
                        break;
                    }
                    case 3:
                    {
                        shipName = "Triple";
                        break;
                    }
                    case 4:
                    {
                        shipName = "Quadro";
                        break;
                    }
                    default: throw new ArgumentOutOfRangeException(nameof(shipSize), "Illegal ship size: " + shipSize + " in " + ship.name);
                }
                */
                foreach (var tile in tiles)
                {
                    shipName += tile;
                }
                //TODO: выдает ошибку при дальнейшей привзяке к событиям
                print("Received tiles " + shipSize + ", ship size " + shipSize);
                var newShip = new Ship(shipName, shipSize);
                foreach (var tile in tiles)
                {
                    PlacedShips.Add(tile, newShip);
                }
            }
        }
    }

    public class BattleStartEvent : UnityEvent<Dictionary<string, Ship>>
    {
    }
}