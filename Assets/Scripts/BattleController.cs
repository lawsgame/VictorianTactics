using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleController : InputHandler
{
    
    [SerializeField] GameObject BattlefieldObject;

    private Camera _camera;
    private Battlefield _battlefield;
    private StateMachine<BattleInterractionState> _battleInterractionStateMachine;


    public Camera GameMainCamera() => _camera;
    public Battlefield Battlefield() => _battlefield;
    public StateMachine<BattleInterractionState> Machine() => _battleInterractionStateMachine;

    private void Awake()
    {
        _battlefield = BattlefieldObject.GetComponent<Battlefield>();
        _camera = Camera.main;
    }

    void Start()
    {
        _battleInterractionStateMachine = new StateMachine<BattleInterractionState>();
        _battleInterractionStateMachine.Push(new FreePlayBIS(this));
    }

    public override void OnTouch(Vector2 mousePos)
    {
        Vector2 worldPos = GameMainCamera().ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Battlefield().Groundmap().WorldToCell(worldPos);
        WorldTile worldTile = Battlefield().Groundmap().GetTile<WorldTile>(tilePos);
        _battleInterractionStateMachine.GetCurrentState().OnTouch(tilePos, worldPos, mousePos, worldTile);
    }

    public override void OnLongTouch(Vector2 mousePos)
    {
        Vector2 worldPos = GameMainCamera().ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Battlefield().Groundmap().WorldToCell(worldPos);
        WorldTile worldTile = Battlefield().Groundmap().GetTile<WorldTile>(tilePos);
        _battleInterractionStateMachine.GetCurrentState().OnLongTouch(tilePos, worldPos, mousePos, worldTile);
    }

    public override void OnPan(Vector2 mousePos, Vector2 mouseDl)
    {
        Vector2 worldPos = GameMainCamera().ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldDl = new Vector2(0, 0);
        _battleInterractionStateMachine.GetCurrentState().OnPan(worldPos, mousePos, worldDl, mouseDl) ;
    }
}
