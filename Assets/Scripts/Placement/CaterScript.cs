using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Placement
{
    public class CaterScript : MonoBehaviour
    {
        [FormerlySerializedAs("_colliders")] public List<string> shipTilesList = new List<string>();
        public List<string> shipCollisions = new();
        private SpriteRenderer stateSprite;
        
        public int shipSize;
        public bool _isMoving;


        public void OnEnable()
        {
            stateSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            stateSprite.color = new Color(0f, 0f, 1, 0.5f);
            SetIsMoving(true);
            
        }

        private void SetIsMoving(bool value)
        {
            _isMoving = value;
            if (_isMoving)
            {
                stateSprite.enabled = true;
            }
            else
            {
                stateSprite.enabled = false;
            }
        }

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

        private void OnMouseUp()
        {
            print("OnMouseUp");
            if (shipTilesList.Count == shipSize) SetIsMoving(false);
            //TODO: RaycastFilter попоробовать. Если столкновение с другим кораблем при отпускании мыши, то остаемся в режиме движения
        }

        private void OnMouseDown()
        {
            GameObject.Find("RotateButtonSwitch").SendMessage("ChangeActiveShip", gameObject);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (!_isMoving) return;
            shipTilesList.Clear();
            print("ship exit tile " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Ship") || collision.gameObject.CompareTag("Ship Collision"))
            {
                stateSprite.color = new Color(0f, 0f, 1, 0.5f);
                return;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isMoving) return;
            shipTilesList.Clear();
            print("ship collide with " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Ship") || collision.gameObject.CompareTag("Ship Collision")) return;
            shipTilesList.Add(collision.gameObject.name);
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (!_isMoving) return;
            if (collision.gameObject.CompareTag("Ship") || collision.gameObject.CompareTag("Ship Collision"))
            {
                shipCollisions.Clear();
                stateSprite.color = new Color(1f, 0f, 0f, 0.5f);
                SetIsMoving(true);
                return;
            }
            if (!shipTilesList.Contains(collision.gameObject.name))
            {
                shipTilesList.Add(collision.gameObject.name);
            }
                
        }
    }
}