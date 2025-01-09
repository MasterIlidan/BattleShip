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
        [SerializeField] public GameObject One;
        [SerializeField] public GameObject Double;
        [SerializeField] public GameObject Triple;
        [SerializeField] public GameObject Quadro;
        

        public void OnClickStartGame()
        {
            PlacedShips.Clear();
            OnPrepareGameStart();
            BattleStartEvent.Invoke(PlacedShips);
        }

        void OnPrepareGameStart()
        {
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
            
            Vector3 difference = new Vector3(37.023f, 0f, 0f);
            
            foreach (var ship in ships)
            {
                var shipSize = ship.GetComponent<CaterScript>().ShipSize;
                var tiles = ship.GetComponent<CaterScript>().shipTilesList;
                var shipName = ship.name;
                if (tiles.Count != shipSize)
                    throw new ArgumentException("Ship size mismatch in ship: " + ship.name + " Value: "
                                                + tiles.Count + " Expected: "
                                                + shipSize);
                GameObject prefab;
                switch (shipSize)
                {
                    case 1:
                    {
                        prefab = One;
                        break;
                    }
                    case 2:
                    {
                        prefab = Double;
                        break;
                    }
                    case 3:
                    {
                        prefab = Triple;
                        break;
                    }
                    case 4:
                    {
                        prefab = Quadro;
                        break;
                    }
                    default: throw new ArgumentOutOfRangeException(nameof(shipSize), "Illegal ship size: " + shipSize + " in " + ship.name);
                }
                foreach (var tile in tiles)
                {
                    shipName += tile;
                }
                Instantiate(prefab, ship.transform.position + difference, ship.transform.rotation).transform.SetParent(GameObject.FindGameObjectWithTag("Player Ships").transform);
                /*print("Received tiles " + shipSize + ", ship size " + shipSize);
                var newShip = new Ship(shipName, shipSize);
                foreach (var tile in tiles)
                {
                    PlacedShips.Add(tile, newShip);
                }*/
            }
        }
    }

    public class BattleStartEvent : UnityEvent<Dictionary<string, Ship>>
    {
    }
}