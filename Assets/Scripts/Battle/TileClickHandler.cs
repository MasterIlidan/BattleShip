using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class TileClickHandler : MonoBehaviour
    {
        public UnityEvent<GameObject> onTileClickedEvent;
        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().onStateChanged.AddListener(SubscribeOnTileClick);
            /*GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
            foreach (var tile in tiles)
            {
                tile.GetComponent<TileScript>().onTileClick.AddListener(OnTileClicked);
            }*/
        }

        private void SubscribeOnTileClick(GameState state)
        {
            if (state != GameState.Battle) return;
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
            foreach (var tile in tiles)
            {
                tile.GetComponent<TileScript>().onTileClick.AddListener(OnTileClicked);
            }
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().onStateChanged.RemoveListener(SubscribeOnTileClick);
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
            foreach (var tile in tiles)
            {
                tile.GetComponent<TileScript>().onTileClick.RemoveListener(OnTileClicked);
            }
        }

        private void OnTileClicked(GameObject tile)
        {
            print("tile clicked " + tile.name);
            onTileClickedEvent.Invoke(tile);
        }
    }

}
