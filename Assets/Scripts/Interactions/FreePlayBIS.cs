using System;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayBIS : BattleInterractionState
{
    private Unit _selectedUnit;
    private int moveAreaId;
   

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

            Battle battle = Controller.Battle;
            Unit touchedUnit = battle.GetUnitFrom(cellPos);
            if (touchedUnit != null)
            {
                _selectedUnit = touchedUnit;
                Debug.Log("New selected unit: " + touchedUnit.name);
            }

            if (touchedUnit == null && _selectedUnit != null && worldTile.Traversable)
            {
                // move unit
                DoCommand(new DisplaceCommand(Controller.Battle.Battlefield, _selectedUnit, cellPos));

                // show were the unit can move
                Controller.AreaHandler.Remove(moveAreaId);
                moveAreaId = Controller.AreaHandler.Create(battle, new TraversableArea(battle, _selectedUnit), AreaType.MOVE);
                Debug.Log("Area Created with ID: " + moveAreaId);
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
            Battle battle = Controller.Battle;
            List<Vector3Int> path = Tactics.Pathfinder.Algorithm.GetShortestPath(battle, _selectedUnit, cellPos);
            Debug.Log(string.Join(" <- ", path));

            // show path
            Controller.AreaHandler.Remove(moveAreaId);
            moveAreaId = Controller.AreaHandler.Create(Controller.Battle, new GenericArea(Controller.Battle, path), AreaType.MOVE);
            Debug.Log("Area Created with ID: " + moveAreaId);
            
        }else if (worldTile != null)
        {
            // compute path
            Battle battle = Controller.Battle;
            List<Vector3Int> path = Tactics.Pathfinder.Algorithm.GetShortestPath(battle, new Vector3Int(0, 2, 0), cellPos);
            Debug.Log(string.Join(" <- ", path));

            // show path
            Controller.AreaHandler.Remove(moveAreaId);
            moveAreaId = Controller.AreaHandler.Create(Controller.Battle, new GenericArea(Controller.Battle, path), AreaType.MOVE);
            Debug.Log("Area Created with ID: " + moveAreaId);

        }
    }
}
