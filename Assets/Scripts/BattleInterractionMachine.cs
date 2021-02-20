using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleInterractionMachine : MonoBehaviour
{
    

    private Battlefield _battlefield;

    private StateMachine<BattleInterractionState> _stateMachine;

    public Battlefield Battlefield() => _battlefield;

    private void Awake()
    {
        GameObject battlefieldGameObject = GameObject.FindGameObjectWithTag("BattleField");
        _battlefield = battlefieldGameObject.GetComponent<Battlefield>();
    }

    void Start()
    {
        _stateMachine = new StateMachine<BattleInterractionState>();
        _stateMachine.Push(new FreePlayBIS(this));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = _battlefield.GroundTilemap().WorldToCell(mousePosition);
            _stateMachine.GetCurrentState().handleInput(clickedCell, mousePosition);
        }
    }
}
