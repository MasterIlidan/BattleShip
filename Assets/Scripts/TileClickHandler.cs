using System;
using System.ComponentModel;
using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    

    public void OnTileClicked(String name)
    {
        print("tile clicked " + name);
    }
}
