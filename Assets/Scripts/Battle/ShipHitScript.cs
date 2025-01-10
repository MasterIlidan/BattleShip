using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;

public class ShipHitScript : MonoBehaviour
{
    public UnityEvent<ShipHitScript> OnHitEvent;
    public bool isHitFlag = false;
    private void OnMouseDown()
    {
        OnShipDamage();
    }

    public void OnShipDamage()
    {
        if (isHitFlag)
        {
            Debug.LogWarning("Attempting to hit damaged ship part");
            return;
        }
        //isHitFlag = true;
        OnHitEvent.Invoke(this);
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
    
}
