using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CaterScript : MonoBehaviour
{ [FormerlySerializedAs("_colliders")] public List<String> shipTilesList = new List<String>();
    public List<String> shipCollisions = new List<String>();
    public int ShipSize;
    public void OnMouseDrag()
    {
        //если курсор на поле, то корабль привязывается к полю. Если нет, то сдедует за курсором
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var raycastHit2D = Physics2D.Raycast(ray, Vector2.zero);
        if (raycastHit2D)
        {
            transform.position = raycastHit2D.collider.gameObject.transform.position;
            return;
        }
        
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        //print("OnMouseDrag cater");
    }

    private void OnMouseUp()
    {
        print("OnMouseUp");
    }

    private void OnMouseDown()
    {
        GameObject.Find("RotateButtonSwitch").SendMessage("ChangeActiveShip", gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        print("ship exit tile " + collision.gameObject.name);
        shipTilesList.Remove(collision.gameObject.name);
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("ship collide with " + collision.gameObject.name);
        shipTilesList.Add(collision.gameObject.name);
    }

    void SendTileData()
    {
        if (shipTilesList.Count != ShipSize)
        {
            Debug.LogWarning(name + " ship tile list size is not equal to ShipSize. Tiles: " + shipTilesList + ", size: " + ShipSize);
            return;
        }
    }
    
}
