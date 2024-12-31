using UnityEngine;

public class RotateShip : MonoBehaviour
{
    GameObject ship;
    public GameObject RotateButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RotateButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ship) return;
        RotateButton.transform.position = new Vector2(
            ship.transform.position.x + 1.5f,
            ship.transform.position.y + 1.5f);
    }

    public void OnClick()
    {
        ship.transform.Rotate(0f, 0f,90f);
    }
    
    //При клике на корабль посылается сообщение в объект RotateButtonSwitch
    void ChangeActiveShip(GameObject ship)
    {
        RotateButton.SetActive(true);
        this.ship = ship;
    }
}
