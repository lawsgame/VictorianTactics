using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleInterractionMachine : MonoBehaviour
{

    private Tilemap _groundTilemap;
    private StateMachine<BattleInterractionState> _stateMachine;

    void Start()
    {
        _groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        _stateMachine = new StateMachine<BattleInterractionState>();
        _stateMachine.Push(new FreePlayBIS(this, _groundTilemap));
        
            
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = _groundTilemap.WorldToCell(mousePosition);
            _stateMachine.GetCurrentState().handleInput(clickedCell, mousePosition);
        }
    }
}
