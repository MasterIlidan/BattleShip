using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    private string _name { get; set; }
    private int _shipSize { get; set; }

    private bool isPlayerTurn;
    private int _hitPoints { get; set; }

    public UnityEvent<Ship> OnShipDestroyed;
    public UnityEvent OnShipDamaged;
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

    public void OnHit(GameObject hitObject)
    {
        if (isPlayerTurn)
        {
            _hitPoints--;
            if (_hitPoints != 0)
            {
                print("You hit the ship!");
                hitObject.GetComponent<SpriteRenderer>().enabled = true;
                hitObject.GetComponent<BoxCollider2D>().enabled = false;
                OnShipDamaged.Invoke();
                return;
            }
            print("Ship destroyed");
            hitObject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            OnShipDestroyed.Invoke(this);
            return;
        }
        print("not player turn");
    }
    public void setup(Ship shipScript)
    {
        _name = shipScript._name;
        _shipSize = shipScript._shipSize;
        _hitPoints = _shipSize;
    }

    public void setup(string name, int shipSize)
    {
        _name = name;
        _shipSize = shipSize;
        _hitPoints = _shipSize;
    }
        
}