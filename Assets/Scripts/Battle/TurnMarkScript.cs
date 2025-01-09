using System;
using UnityEngine;

public class TurnMarkScript : MonoBehaviour
{
    private Vector2[] markPos =
    {
        new Vector2(4, 4.88f),
        new Vector2(-4, 4.88f),
    };
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Battle Controller").GetComponent<BattleController>().OnChangeTurn.AddListener(OnChangePlayerTurn);
    }

    void OnDisable()
    {
        GameObject.FindGameObjectWithTag("Battle Controller").GetComponent<BattleController>().OnChangeTurn.RemoveListener(OnChangePlayerTurn);
    }

    public void OnChangePlayerTurn(bool isPlayerTurn)
    {
        int turn = isPlayerTurn ? (int)MarkPosition.PLAYER : (int)MarkPosition.ENEMY;
        transform.position = markPos[turn];
    }

    enum MarkPosition
    {
        ENEMY = 0, PLAYER = 1 
    }
    
}
