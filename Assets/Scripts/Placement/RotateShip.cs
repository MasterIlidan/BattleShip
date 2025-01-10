using UnityEngine;
using UnityEngine.Serialization;

namespace Placement
{
    public class RotateShip : MonoBehaviour
    {
        private GameObject _ship;
        [FormerlySerializedAs("RotateButton")] public GameObject rotateButton;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rotateButton.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_ship) return;
            rotateButton.transform.position = new Vector2(
                _ship.transform.position.x + 1.5f,
                _ship.transform.position.y + 1.5f);
        }

        public void OnClick()
        {
            _ship.GetComponent<CaterScript>().OnRotate();
        }
    
        //При клике на корабль посылается сообщение в объект RotateButtonSwitch
        void ChangeActiveShip(GameObject ship)
        {
            rotateButton.SetActive(true);
            this._ship = ship;
        }
    }
}
