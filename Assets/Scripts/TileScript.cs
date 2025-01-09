using System;
using UnityEngine;
using UnityEngine.Events;

public class TileScript : MonoBehaviour
{
    public GameObject tileClickHandler;
    public UnityEvent<GameObject> onTileClick;

    private void OnCollisionEnter2D(Collision2D other)
    {
        //print("Collision with " + other.gameObject.name);
    }
    
    public void OnMouseDown()
    {
        //print("Tile clicked "+name);
        onTileClick?.Invoke(gameObject);
    }
}
