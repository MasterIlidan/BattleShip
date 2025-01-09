using System;
using UnityEngine;
using UnityEngine.Events;

public class ShipHitScript : MonoBehaviour
{
    public UnityEvent<GameObject> OnHitEvent;
    
    void OnMouseDown()
    {
        OnHitEvent.Invoke(gameObject);
    }
    //отключить клетки под кораблем
    private void OnTriggerStay(Collider other)
    {
        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        print("tile under ship disabled " + other.gameObject.name);
    }
}
