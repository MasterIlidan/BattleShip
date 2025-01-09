using System;
using System.Collections.Generic;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    private GameObject _gameObject;
    private Ray _ray;
    private RaycastHit _hit;
    static readonly List<string> Tiles = InitTilesList();
    void Start()
    {
        /*_gameObject = GameObject.FindGameObjectWithTag("GameController");

        GameObject tile = GameObject.FindGameObjectWithTag("Tiles");
        Vector3 firstTilVector3 = tile.transform.position;
        char letter = 'A';
        for (int j = 1; j <= 10; j++)
        {
            tile = GameObject.FindGameObjectWithTag("Tiles");
            Vector3 origin = firstTilVector3;
            if (j != 0)
            {
                tile = Instantiate(tile, origin, tile.transform.rotation);
                tile.name = "" + letter + 1;
            }
            for (int i = 2; i <= 10; i++)
            {

                origin.y -= 0.535f;
                tile = Instantiate(tile, origin, tile.transform.rotation);
                tile.name = "" + letter + i;
            }
            letter = (char)(letter + 1);
            firstTilVector3.x += 0.53f;
        }*/
    }
    private static List<string> InitTilesList()
    {
        List<string> tiles = new List<string>();
        char letter = 'A';
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                tiles.Add(letter.ToString() + j.ToString());
            }
            letter += (char) 1;
        }
        return tiles;
    }

    void Update()
    {
       /* _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            print(_hit.collider.name);
        }
        */
       if (Input.GetMouseButtonDown(0)) {
           print("mouse down");
           _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(_ray, out _hit))
           {
               print(_hit.collider.name);
               
           }
       }
    }
}
