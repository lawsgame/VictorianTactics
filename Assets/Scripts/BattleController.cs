using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleController : MonoBehaviour
{
    
    [SerializeField] GameObject BattlefieldObject;

    private Battlefield _battlefield;
    private StateMachine<BattleInterractionState> _battleInterractionStateMachine;

    public Battlefield Battlefield() => _battlefield;
    public StateMachine<BattleInterractionState> InterractionMachine() => _battleInterractionStateMachine;

    private void Awake()
    {
        _battlefield = BattlefieldObject.GetComponent<Battlefield>();
    }

    void Start()
    {
        _battleInterractionStateMachine = new StateMachine<BattleInterractionState>();
        _battleInterractionStateMachine.Push(new FreePlayBIS(this));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = _battlefield.GroundTilemap().WorldToCell(mousePosition);
            _battleInterractionStateMachine.GetCurrentState().handleInput(clickedCell, mousePosition);
        }
    }
}
