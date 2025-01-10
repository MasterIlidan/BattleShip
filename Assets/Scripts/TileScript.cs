using UnityEngine;
using UnityEngine.Events;

public class TileScript : MonoBehaviour
{
    public GameObject tileClickHandler;
    public UnityEvent<GameObject> onTileClick;

    
    public void OnMouseDown()
    {
        //print("Tile clicked "+name);
        onTileClick?.Invoke(gameObject);
    }
}
