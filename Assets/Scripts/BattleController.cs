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
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = _battlefield.GroundTilemap().WorldToCell(worldPos);
            WorldTile worldTile = _battlefield.GroundTilemap().GetTile<WorldTile>(tilePos);
            _battleInterractionStateMachine.GetCurrentState().OnTouch(tilePos, worldPos, mousePos, worldTile);
        }
    }
}
