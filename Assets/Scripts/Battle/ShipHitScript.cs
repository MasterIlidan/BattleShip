using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class ShipHitScript : MonoBehaviour
{
    public UnityEvent<GameObject> OnHitEvent;
    
    void OnMouseDown()
    {
        OnHitEvent.Invoke(gameObject);
    }

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameManager>().OnStateChanged.AddListener(OnStateChanged);
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameManager>().OnStateChanged.RemoveListener(OnStateChanged);
    }
    private void OnStateChanged(GameState state)
    {
        if (state != GameState.BATTLE) return;
         Collider2D[] results = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.2f, 0.2f), 0);
         foreach (var collider in results)
         {
             if (!collider.CompareTag("Tiles")) continue;
             print("Disabled tile" + collider.gameObject.name);
             if (collider.gameObject.TryGetComponent<BoxCollider2D>(out var type))
             {
                 type.enabled = false;
             };
         }
    }
    //отключить клетки под кораблем
    /*public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tiles"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            print("OnTriggerStay tile under ship disabled " + other.gameObject.name);
        }
    }*/
    
}
