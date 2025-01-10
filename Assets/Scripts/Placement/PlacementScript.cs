using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Placement
{
    public class PlacementScript : MonoBehaviour
    {
        public List<Ship> placedShips = new();

        public UnityEvent<List<Ship>> battleStartEvent;
        [FormerlySerializedAs("One")] [SerializeField] public GameObject one;
        [FormerlySerializedAs("Double")] [SerializeField] public GameObject @double;
        [FormerlySerializedAs("Triple")] [SerializeField] public GameObject triple;
        [FormerlySerializedAs("Quadro")] [SerializeField] public GameObject quadro;


        public void OnClickStartGame()
        {
            placedShips.Clear();
            OnPrepareGameStart();
            battleStartEvent.Invoke(placedShips);
        }

        void OnPrepareGameStart()
        {
            GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");


            List<string> shipNames = new();
            List<GameObject> shipsToInstantiate = new();
            List<GameObject> prefabList = new();

            foreach (var ship in ships)
            {
                var shipSize = ship.GetComponent<CaterScript>().shipSize;
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
                        prefab = one;
                        break;
                    }
                    case 2:
                    {
                        prefab = @double;
                        break;
                    }
                    case 3:
                    {
                        prefab = triple;
                        break;
                    }
                    case 4:
                    {
                        prefab = quadro;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(shipSize),
                            "Illegal ship size: " + shipSize + " in " + ship.name);
                }
                
                foreach (var tile in tiles)
                {
                    shipName += tile;
                }
                
                prefabList.Add(prefab);
                shipsToInstantiate.Add(ship);
                shipNames.Add(shipName);
            }
            InstantiateShips(shipsToInstantiate,
                prefabList,
                shipNames);
            SetEnemyShips();
        }

        private void InstantiateShips(List<GameObject> ships,
            List<GameObject> prefabList,
            List<string> namesList)
        {
            Vector3 difference = new Vector3(-37.0063f, 0.066f, 0f);
            for (int i = 0; i < ships.Count; i++)
            {
                GameObject ship = ships[i];
                GameObject prefab = prefabList[i];
                string shipName = namesList[i];

                GameObject newShip = Instantiate(prefab,
                    ship.transform.position - difference,
                    ship.transform.rotation);
                newShip.transform.SetParent(GameObject.FindGameObjectWithTag("Player Ships").transform);
                newShip.name = shipName;

                Ship newShipScript = newShip.GetComponent<Ship>();
                CaterScript caterScript = ship.GetComponent<CaterScript>();

                newShipScript.Setup(shipName, caterScript.shipSize, true);
            }
        }

        private void SetEnemyShips()
        {
            Vector3 offset = new Vector3(7.996694f, 0f, 0f);
            GameObject playerShips = GameObject.FindGameObjectWithTag("Player Ships");
            GameObject enemyShips = GameObject.FindGameObjectWithTag("Enemy Ships");

            for (var i = 0; i < playerShips.transform.childCount; i++)
            {
                GameObject playerShip = playerShips.transform.GetChild(i).gameObject;
                Ship playerShipScript = playerShip.GetComponent<Ship>();


                GameObject enemyShip = Instantiate(playerShip,
                    playerShip.transform.position - offset,
                    Quaternion.identity);
                //enemyShip.SetActive(false);
                enemyShip.GetComponent<Ship>().Setup(playerShipScript, false);
                print("copy player ship " + enemyShip.name + " to enemy field");
                for (int j = 0; j < enemyShip.transform.childCount; j++)
                {
                    GameObject obj = enemyShip.transform.GetChild(j).gameObject;
                    if (obj.name != "ShipSprite") continue;
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                }
                enemyShip.transform.SetParent(enemyShips.transform);
                //enemyShip.SetActive(true);
            }

            enemyShips.transform.rotation = new Quaternion(0f, 0f, 90f, 0f);
        }
    }
}