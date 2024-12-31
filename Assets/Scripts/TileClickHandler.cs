using System;
using System.ComponentModel;
using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    

    public void OnTileClicked(string name)
    {
        print("tile clicked " + name);
    }
}
