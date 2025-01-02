using System.Collections.Generic;
using System.Linq;
using Mono.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Placement
{
    public class CaterScript : MonoBehaviour
    { [FormerlySerializedAs("_colliders")] public List<string> shipTilesList = new List<string>();
        public List<string> shipCollisions = new List<string>();
        public int ShipSize;
        
        public bool IsMoving = true;
        private SpriteRenderer _statusSprite;
        public void OnMouseDrag()
        {
            SetIsMoving(true);
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

        void OnEnable()
        {
            _statusSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _statusSprite.color = new Color(0f, 0.3f, 01f, 0.5f);
        }

        private void OnMouseUp()
        {
            
            List<Collider2D> col = new List<Collider2D>();
            col.Add(GetComponent<Collider2D>());
            int colliderCount = GetComponent<Rigidbody2D>().GetContacts(col);
            
            
            if (colliderCount != ShipSize)
            {
                shipTilesList.Clear();
                print("ship size " + ShipSize + " and colliders " + colliderCount +" mismatch");
                return;
            }

            SetIsMoving(false);
            print("new method collided count " +colliderCount);
        }

        private void SetIsMoving(bool value)
        {
            IsMoving = value;
            if (IsMoving)
            {
                _statusSprite.enabled = true;
            }
            else
            {
                _statusSprite.enabled = false;
            }
        }

        private void OnMouseDown()
        {
            shipTilesList.Clear();
            Ray2D ray = new Ray2D(new Vector2(transform.position.x, transform.position.y), Vector2.zero);
            
            
            GameObject.Find("RotateButtonSwitch").SendMessage("ChangeActiveShip", gameObject);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (!IsMoving) return;
            //print("ship exit tile " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Tiles"))
            {
                shipTilesList.Remove(collision.gameObject.name);
            }
            else
            {
                _statusSprite.color = new Color(0f, 0.3f, 01f, 0.5f);
            }
        
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsMoving) return;
            //print("ship collide with " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Tiles"))
            {
                shipTilesList.Add(collision.gameObject.name);
            }
        }


        void OnTriggerStay2D(Collider2D collision)
        {
            if (!IsMoving) return;
            if (collision.gameObject.CompareTag("Ship") ||
                collision.gameObject.CompareTag("Ship Collision"))
            {
                _statusSprite.color = new Color(1f, 0f, 0f, 0.5f);
            }
        }
    
    }
}
