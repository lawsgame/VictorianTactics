using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinder;

public class PathFinderTest : MonoBehaviour
{


    [SerializeField] private int initX;
    [SerializeField] private int initY;

    private Battlefield _battlefield;
    private Vector3Int _initGridPos;

    private StateMachine<BattleInterractionState> _stateMachine;

    public Battlefield Battlefield() => _battlefield;

    private void Awake()
    {
        GameObject battlefieldGameObject = GameObject.FindGameObjectWithTag("BattleField");
        _battlefield = battlefieldGameObject.GetComponent<Battlefield>();
        _initGridPos = new Vector3Int(initX, initY, 0);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = _battlefield.GroundTilemap().WorldToCell(mousePosition);
            Debug.Log(string.Join(" <- ", Algorithm.GetShortestPath(_battlefield.GroundTilemap(), _initGridPos, clickedCell)));
            
        }
    }
}
