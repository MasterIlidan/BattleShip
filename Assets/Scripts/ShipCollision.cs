using Unity.VisualScripting;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        print(name + " OnCollisionEnter with " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Tiles"))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(name + " OnCollisionExit with " + collision.gameObject.name);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
