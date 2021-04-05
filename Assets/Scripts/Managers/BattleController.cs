using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleController : InputHandler
{
    
    [SerializeField] GameObject BattleObject;

    private Camera _camera;
    private Battle _battle;
    private AreaHandler _areaHandler;
    private BattleInterractionState _currentState;
    private StateMachine<BattleInterractionState> _battleInterractionStateMachine;
    
    public Camera GameMainCamera => _camera;
    public Battle Battle => _battle;
    public AreaHandler AreaHandler => _areaHandler;
    public StateMachine<BattleInterractionState> Machine => _battleInterractionStateMachine;

    private void Awake()
    {
        _battle = BattleObject.GetComponent<Battle>();
        _areaHandler = GetComponent<AreaHandler>();
        _camera = Camera.main;
    }

    void Start()
    {
        _battleInterractionStateMachine = new StateMachine<BattleInterractionState>();
        _currentState = new FreePlayBIS(this);
        _battleInterractionStateMachine.Push(_currentState);
    }

    protected override void Update()
    {
        base.Update();
    } 

    public override void OnTouch(Vector2 mousePos)
    {
        Vector2 worldPos = GameMainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Battle.Battlefield.WorldToCell(worldPos);
        WorldTile worldTile = Battle.Battlefield.GetTile<WorldTile>(tilePos);
        _battleInterractionStateMachine.GetCurrentState().OnTouch(tilePos, worldPos, mousePos, worldTile);
    }

    public override void OnLongTouch(Vector2 mousePos)
    {
        Vector2 worldPos = GameMainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Battle.Battlefield.WorldToCell(worldPos);
        WorldTile worldTile = Battle.Battlefield.GetTile<WorldTile>(tilePos);
        _battleInterractionStateMachine.GetCurrentState().OnLongTouch(tilePos, worldPos, mousePos, worldTile);
    }

    public override void OnDoubleTap(Vector2 mousePos)
    {
        Vector2 worldPos = GameMainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Battle.Battlefield.WorldToCell(worldPos);
        WorldTile worldTile = Battle.Battlefield.GetTile<WorldTile>(tilePos);
        _battleInterractionStateMachine.GetCurrentState().OnDoubleTap(tilePos, worldPos, mousePos, worldTile);
    }

    public override void OnPan(Vector2 mousePos, Vector2 mouseDl)
    {
        Vector2 worldPos = GameMainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldDl = new Vector2(0, 0);
        _battleInterractionStateMachine.GetCurrentState().OnPan(worldPos, mousePos, worldDl, mouseDl) ;
    }

    public override void OnAllowedKeyDown(KeyCode code)
    {
        _battleInterractionStateMachine.GetCurrentState().OnKeyDown(code);
    }
}
