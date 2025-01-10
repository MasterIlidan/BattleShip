using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    private string _name { get; set; }
    private int _shipSize { get; set; }

    private bool isPlayerTurn;
    public bool isPlayerShip { get; private set; }
    private int _hitPoints { get; set; }

    public UnityEvent<Ship> OnShipDestroyed;
    public UnityEvent<Ship> OnShipDamaged;
    public Ship(string name, int shipSize)
    {
        _name = name;
        _shipSize = shipSize;
        _hitPoints = shipSize;
    }

    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Battle Controller").GetComponent<BattleController>().OnChangeTurn.AddListener(OnTurnChange);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(typeof(BoxCollider2D), out var boxCollider2DComponent))
            {
                transform.GetChild(i).GetComponent<ShipHitScript>().OnHitEvent.AddListener(OnHit);
            }
        }
    }

    void OnTurnChange(bool isPlayerTurn)
    {
        this.isPlayerTurn = isPlayerTurn;
    }

    public void OnHit(ShipHitScript hitObject)
    {
        if (isPlayerTurn)
        {
            _hitPoints--;
            hitObject.isHitFlag = true;
            if (_hitPoints != 0)
            {
                print("You hit the ship!");
                hitObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                hitObject.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                OnShipDamaged.Invoke(this);
                return;
            }
            print("Ship destroyed");
            hitObject.gameObject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            OnShipDestroyed.Invoke(this);
            return;
        }
        print("not player turn");
    }
    public void setup(Ship shipScript, bool isPlayerShip)
    {
        _name = shipScript._name;
        _shipSize = shipScript._shipSize;
        _hitPoints = _shipSize;
        this.isPlayerShip = isPlayerShip;
    }

    public void setup(string name, int shipSize, bool isPlayerShip)
    {
        _name = name;
        _shipSize = shipSize;
        _hitPoints = _shipSize;
        this.isPlayerShip = isPlayerShip;
    }
        
}