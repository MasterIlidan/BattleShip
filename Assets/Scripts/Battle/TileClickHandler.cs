using System;
using System.Collections.Generic;
using System.ComponentModel;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class TileClickHandler : MonoBehaviour
{
    public UnityEvent<GameObject> OnTileClickedEvent;
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.AddListener(Subscribe);
        /*GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tiles)
        {
            tile.GetComponent<TileScript>().onTileClick.AddListener(OnTileClicked);
        }*/
    }

    public void Subscribe(GameState state)
    {
        if (state != GameState.BATTLE) return;
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tiles)
        {
            tile.GetComponent<TileScript>().onTileClick.AddListener(OnTileClicked);
        }
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.RemoveListener(Subscribe);
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tiles)
        {
            tile.GetComponent<TileScript>().onTileClick.RemoveListener(OnTileClicked);
        }
    }

    public void OnTileClicked(GameObject tile)
    {
        print("tile clicked " + tile.name);
        OnTileClickedEvent.Invoke(tile);
    }
}
