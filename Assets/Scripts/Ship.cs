using Battle;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Ship : MonoBehaviour
{
    private string Name { get; set; }
    private int ShipSize { get; set; }
    public bool IsPlayerShip { get; private set; }
    private int HitPoints { get; set; }

    [FormerlySerializedAs("OnShipDestroyed")] public UnityEvent<Ship> onShipDestroyed;
    [FormerlySerializedAs("OnShipDamaged")] public UnityEvent<Ship> onShipDamaged;
    
    public Ship(string name, int shipSize)
    {
        Name = name;
        ShipSize = shipSize;
        HitPoints = shipSize;
    }

    private void OnEnable()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(typeof(BoxCollider2D), out var boxCollider2DComponent))
            {
                transform.GetChild(i).GetComponent<ShipHitScript>().onHitEvent.AddListener(OnHit);
            }
        }
    }

    private void OnHit(ShipHitScript hitObject)
    {
        HitPoints--;
        hitObject.isHitFlag = true;
        DisableHitMark(hitObject.gameObject);
        if (HitPoints != 0)
        {
            print("You hit the ship!");
            onShipDamaged.Invoke(this);
            return;
        }

        print("Ship destroyed");
        hitObject.gameObject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        onShipDestroyed.Invoke(this);
    }

    private static void DisableHitMark(GameObject hitObject)
    {
        hitObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hitObject.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Setup(Ship shipScript, bool isPlayerShip)
    {
        Name = shipScript.Name;
        ShipSize = shipScript.ShipSize;
        HitPoints = ShipSize;
        this.IsPlayerShip = isPlayerShip;
    }

    public void Setup(string shipName, int shipSize, bool isPlayerShip)
    {
        Name = shipName;
        ShipSize = shipSize;
        HitPoints = ShipSize;
        this.IsPlayerShip = isPlayerShip;
    }
        
}