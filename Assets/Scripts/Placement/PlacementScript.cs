using System;
using System.Collections.Generic;
using UnityEngine;

namespace Placement
{
    public class PlacementScript : MonoBehaviour
    {
        public Dictionary<string, Ship> PlacedShips = new();
    
        public void OnClickStartGame()
        {
            PlacedShips.Clear();
            OnPrepareGameStart();
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
                    throw new ArgumentException("Ship size mismatch Value: "
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

                print("Received tiles " + shipSize + ", ship size " + shipSize);
                var newShip = new Ship(shipName, shipSize);
                foreach (var tile in tiles)
                {
                    PlacedShips.Add(tile, newShip);
                }
            }
        }
    }

    public class Ship
    {
        private string _name;
        private int _shipSize;
        private int _hitPoints;

        public Ship(string name, int shipSize)
        {
            _name = name;
            _shipSize = shipSize;
            _hitPoints = shipSize;
        }
    }
}