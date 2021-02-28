using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreePlayBIS : BattleInterractionState
{
    private Unit _selectedUnit;
   

    public FreePlayBIS(BattleController controller) : base(controller)
    {
    }

    public override void Init() => Debug.Log("Init FreePlayBIS");
    public override void Dispose() =>  Debug.Log("Dispode FreePlayBIS");
    public override void End() =>  Debug.Log("End FreePlayBIS");

    public override void OnPan(Vector2 worldPos, Vector2 mousePos, Vector2 mouseDl, Vector2 worldDl) => Debug.Log("Pan on " + mousePos);


    public override void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile)
    {
        Unit touchedUnit = Controller.Battlefield.GetUnitFrom(cellPos);
        Debug.Log("Long touch on " + touchedTile + ((touchedUnit !=null) ? " where "+ touchedUnit+" is standing" : ""));
    }


    public override void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile worldTile)
    {
        Debug.Log(string.Format("Touch on {0} / {1} / {2} / {3}", cellPos, worldPos, mousePosition, worldTile));

        if (worldTile != null)
        {
            // check if one of the unit has been selected
            Unit touchedUnit = Controller.Battlefield.GetUnitFrom(cellPos);
            
            if(touchedUnit != null)
            { 
                _selectedUnit = touchedUnit;
                Debug.Log("New selected unit: " + touchedUnit.name);
            }
            

            // if an empty tile has been selected, move the selected unit to this tile
            if (touchedUnit == null && _selectedUnit != null && worldTile.Model.Traversable)
            {
                DisplaceCommand currentCommand = new DisplaceCommand(Controller.Battlefield.Groundmap, _selectedUnit, cellPos);
                if (currentCommand.IsExecutable())
                    currentCommand.Apply();
            }
        }
    }

    
}
