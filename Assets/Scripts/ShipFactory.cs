using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipFactory : MonoBehaviour
{
    private int _count;
    [FormerlySerializedAs("ShipsCount")] public int shipsCount;
    
    public int Count
    { 
        get
        {
            return _count;
        }
        set
        {
            _count = value;
            uiText.GetComponent<TextMeshProUGUI>().SetText(_count.ToString());
        }
    }
    public GameObject uiText;
    
    
    [FormerlySerializedAs("ShipPrefab")]  public GameObject shipPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Count = shipsCount;
        //uiText.GetComponent<TextMeshPro>().SetText(Count.ToString());
        
    }
    public void OnMouseDown()
    {
        if (Count == 0)
        {
            print("all ships have been created");
            return;
        }
        Count--;
        Instantiate(shipPrefab, transform.position, Quaternion.identity);
        print("Ship SPAWNED");
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
    }
}
