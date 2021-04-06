﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayBIS : BattleInterractionState
{
    private Unit _selectedUnit;
    private int moveAreaId;
    private int attackAreaId;
   

    public FreePlayBIS(BattleController controller) : base(controller)
    {
    }

    public override void Init() => Debug.Log("Init FreePlayBIS");
    public override void Dispose() =>  Debug.Log("Dispose FreePlayBIS");
    public override void End() =>  Debug.Log("End FreePlayBIS");

    public override void OnPan(Vector2 worldPos, Vector2 mousePos, Vector2 mouseDl, Vector2 worldDl) => Debug.Log("Pan on " + mousePos);


    public override void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile)
    {
        TestPathfinding(cellPos, worldPos, MousePosition, touchedTile);
    }


    public override void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile worldTile)
    {
        TestDisplaceCommandAndTraversableArea(cellPos, worldPos, mousePosition, worldTile);
    }


    public override void OnDoubleTap(Vector3Int tilePos, Vector2 worldPos, Vector2 mousePos, WorldTile worldTile)
    {
        Controller.AreaHandler.RemoveAll();
        if (UndoLastCommand())
            Debug.Log("undone");
        else
            Debug.Log("undo aborted");
    }

    public override void OnKeyDown(KeyCode code)
    {
        if(_selectedUnit != null)
        {
            switch (code)
            {
                case KeyCode.A: _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Attack, Data.Orientation.East); break;
                case KeyCode.Z: _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.LevelUp, Data.Orientation.South); break;
                case KeyCode.E: _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Idle, Data.Orientation.West); break;
                case KeyCode.Q: _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Wound, Data.Orientation.North); break;
                case KeyCode.S: 
                    _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Wound, Data.Orientation.North); 
                    _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Die, Data.Orientation.North); 
                    break;
                case KeyCode.D: 
                    _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Wound, Data.Orientation.North);
                    _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Die, Data.Orientation.North);
                    _selectedUnit.Scheduler.QueueNext(UnitAnimation.Key.Dead, Data.Orientation.North);
                    break;
            }

        }
    }

    private void TestDisplaceCommandAndTraversableArea(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile worldTile)
    {

        Debug.Log(string.Format("Touch on {0} / {1} / {2} / {3}", cellPos, worldPos, mousePosition, worldTile));

        if (worldTile != null)
        {

            Battlefield battle = Controller.Battlefield;
            Unit touchedUnit = battle.GetUnitFrom(cellPos);
            if (touchedUnit != null)
            {
                _selectedUnit = touchedUnit;
                Debug.Log("New selected unit: " + touchedUnit.name);
            }

            if (touchedUnit == null && _selectedUnit != null && worldTile.Traversable)
            {
                // move unit
                DoCommand(new DisplaceCommand(Controller.Battlefield.Map, _selectedUnit, cellPos));

                // show were the unit can move
                Controller.AreaHandler.Remove(moveAreaId);
                IAreaModel moveArea = new TraversableArea(battle, _selectedUnit);
                moveAreaId = Controller.AreaHandler.Create(battle, moveArea, AreaType.MOVE);
                Debug.Log("Area Created with ID: " + moveAreaId);

                Controller.AreaHandler.Remove(attackAreaId);
                List<Vector3Int> attackTargetCells = ActionAreaFinder.Algorithm.FindAttackArea(moveArea.GetCells(), battle, _selectedUnit);
                attackAreaId = Controller.AreaHandler.Create(battle, new GenericArea(battle, attackTargetCells), AreaType.ATTACK);
            }
        }
    }

    private void TestPathfinding(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile worldTile)
    {
        Debug.Log(string.Format("Long Touch on {0} / {1} / {2} / {3}", cellPos, worldPos, mousePosition, worldTile));
        
        if (worldTile != null && _selectedUnit != null)
        {
            Debug.Log("UNIT PATH");

            // compute path
            Battlefield bf = Controller.Battlefield;
            List<Vector3Int> path = Pathfinder.Algorithm.GetShortestPath(bf, _selectedUnit, cellPos);
            Debug.Log(string.Join(" <- ", path));

            // show path
            Controller.AreaHandler.Remove(moveAreaId);
            moveAreaId = Controller.AreaHandler.Create(Controller.Battlefield, new GenericArea(Controller.Battlefield, path), AreaType.MOVE);
            Debug.Log("Area Created with ID: " + moveAreaId);
            
        }
        else if (worldTile != null)
        {
            // compute path
            Battlefield bf = Controller.Battlefield;
            int moveRange = 20;
            int partyNumber = 0;
            Vector3Int startingCell = new Vector3Int(0, 2, 0);
            List<Vector3Int> path = Pathfinder.Algorithm.GetShortestPath(bf, startingCell, cellPos, moveRange, partyNumber);
            Debug.Log(string.Join(" <- ", path));

            // show path
            Controller.AreaHandler.Remove(moveAreaId);
            moveAreaId = Controller.AreaHandler.Create(Controller.Battlefield, new GenericArea(Controller.Battlefield, path), AreaType.MOVE);
            Debug.Log("Area Created with ID: " + moveAreaId);

        }
    }
}
