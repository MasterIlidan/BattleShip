using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class ShipHitScript : MonoBehaviour
    {
        public UnityEvent<ShipHitScript> onHitEvent;
        public bool isHitFlag;
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
            onHitEvent.Invoke(this);
        }

        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().onStateChanged.AddListener(OnStateChanged);
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<GameManager>().onStateChanged.RemoveListener(OnStateChanged);
        }
        private void OnStateChanged(GameState state)
        {
            if (state != GameState.Battle) return;
            Collider2D[] results = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.2f, 0.2f), 0);
            foreach (var collidingObject in results)
            {
                if (!collidingObject.CompareTag("Tiles")) continue;
                print("Disabled tile" + collidingObject.gameObject.name);
                if (collidingObject.gameObject.TryGetComponent<BoxCollider2D>(out var type))
                {
                    type.enabled = false;
                }
            }
        }
    
    }
}
