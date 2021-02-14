using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleInterractionMachine : MonoBehaviour
{

    private Tilemap _groundTilemap;
    private StateMachine<BattleInterractionState> _stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new StateMachine<BattleInterractionState>();
        _stateMachine.Push(new FreePlayBIS());
        _groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
            
    }

    // Update is called once per frame
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
