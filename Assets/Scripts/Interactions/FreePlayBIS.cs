using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreePlayBIS : BattleInterractionState
{
    private GameObject _selectedUnit;
    private Vector3Int _selectedCell;

    public FreePlayBIS(BattleController controller) : base(controller)
    {
    }

    public override void Dispose()
    {
        Debug.Log("Dispode FreePlayBIS");
    }

    public override void End()
    {
        Debug.Log("End FreePlayBIS");
    }

    public override void handleInput(Vector3Int clickCellPos, Vector2 MousePosition)
    {
        // register which cell is clicked
        _selectedCell = clickCellPos;
        Debug.Log("clicked screen location: " + MousePosition+" / clicked cell: "+clickCellPos);


        Tilemap groundMap = Controller.Battlefield().GroundTilemap();
        List<Unit> units = Controller.Battlefield().Units();

        TileBase tile = groundMap.GetTile(clickCellPos);
        WorldTile worldTile = null;
        if (tile is WorldTile){
            worldTile = tile as WorldTile;
            Debug.Log(worldTile.ToString());
        }

        if(worldTile != null)
        {
            // check if one of the unit has been selected
            bool newSelectedUnit = false;
            Vector3Int unitCellPos;
            foreach (Unit unit in units)
            {
                unitCellPos = groundMap.WorldToCell(unit.gameObject.transform.position);

                if (clickCellPos.Equals(unitCellPos))
                {
                    newSelectedUnit = true;
                    _selectedUnit = unit.gameObject;
                    Debug.Log("New selected unit: " + unit.name);
                    break;
                }
            }

            // if an empty tile has been selected, move the selected unit to this tile
            if (!newSelectedUnit && _selectedUnit != null && worldTile.Model.Traversable)
            {
                Vector3 unitNewWorldPos = groundMap.CellToWorld(clickCellPos);
                unitNewWorldPos.y += 0.25f;
                _selectedUnit.transform.position = unitNewWorldPos;
                Debug.Log(string.Format("{0} unit displaced to {1}", _selectedUnit.name, _selectedCell));
            }
        }
        
    }

    public override void Init()
    {
        Debug.Log("Init FreePlayBIS");
    }
}
