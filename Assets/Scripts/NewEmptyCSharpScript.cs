using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    private GameObject _gameObject;
    private Ray _ray;
    private RaycastHit _hit;

    void Update()
    {
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
