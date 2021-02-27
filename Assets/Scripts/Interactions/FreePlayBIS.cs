using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreePlayBIS : BattleInterractionState
{
    private GameObject _selectedUnit;
    private Vector3Int _selectedCell;
    private Tilemap _groundMap;

    public FreePlayBIS(BattleController controller) : base(controller)
    {
        _groundMap = controller.Battlefield().Groundmap();
    }

    public override void Init() => Debug.Log("Init FreePlayBIS");
    public override void Dispose() =>  Debug.Log("Dispode FreePlayBIS");
    public override void End() =>  Debug.Log("End FreePlayBIS");

    public override void OnKeyPressed(KeyCode keyCode) => Debug.Log("Key pressed: " + keyCode);
    public override void Pan(Vector2 worldPos, Vector2 MousePosition) => Debug.Log("Pan on " + worldPos);


    public override void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile)
    {
        Debug.Log("Long touch on: " + cellPos + " where *** is standing");
    }


    public override void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile worldTile)
    {
        Debug.Log(string.Format("Inputs {0} / {1} / {2} / {3}", cellPos, worldPos, mousePosition, worldTile));

        _selectedCell = cellPos;
        
        List<Unit> units = Controller.Battlefield().UnitGroup().GetUnits();


        if (worldTile != null)
        {
            // check if one of the unit has been selected
            Unit touchedUnit = Controller.Battlefield().UnitGroup().GetUnitFrom(cellPos);
            
            if(touchedUnit != null)
            { 
                _selectedUnit = touchedUnit.gameObject;
                Debug.Log("New selected unit: " + touchedUnit.name);
            }
            

            // if an empty tile has been selected, move the selected unit to this tile
            if (touchedUnit == null && _selectedUnit != null && worldTile.Model.Traversable)
            {
                Vector3 unitNewWorldPos = _groundMap.CellToWorld(cellPos);
                unitNewWorldPos.y += 0.25f;
                _selectedUnit.transform.position = unitNewWorldPos;
                Debug.Log(string.Format("{0} unit displaced to {1}", _selectedUnit.name, _selectedCell));
            }
        }
    }

    
}
