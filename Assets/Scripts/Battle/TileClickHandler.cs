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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.AddListener(SubscribeOnTileClick);
        /*GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tiles)
        {
            tile.GetComponent<TileScript>().onTileClick.AddListener(OnTileClicked);
        }*/
    }

    private void SubscribeOnTileClick(GameState state)
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnStateChanged.RemoveListener(SubscribeOnTileClick);
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
        foreach (var tile in tiles)
        {
            tile.GetComponent<TileScript>().onTileClick.RemoveListener(OnTileClicked);
        }
    }

    private void OnTileClicked(GameObject tile)
    {
        print("tile clicked " + tile.name);
        OnTileClickedEvent.Invoke(tile);
    }
}
