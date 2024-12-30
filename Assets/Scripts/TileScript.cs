using System;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject tileClickHandler;
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("Collision with " + other.gameObject.name);
    }


    public void OnMouseDown()
    {
        print("Tile clicked "+name);
    }
}
