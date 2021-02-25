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

    public override void Init() => Debug.Log("Init FreePlayBIS");
    public override void Dispose() =>  Debug.Log("Dispode FreePlayBIS");
    public override void End() =>  Debug.Log("End FreePlayBIS");

    public override void OnKeyPressed(KeyCode keyCode) => Debug.Log("Key pressed: " + keyCode);
    public override void Pan(Vector2 worldPos, Vector2 MousePosition) => Debug.Log("Pan on " + worldPos);

    public override void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile)
    {

        Debug.Log("Long touch on: " + cellPos + " where *** is standing");
    }

    public override void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePosition, WorldTile touchedTile)
    {
        Debug.Log(string.Format("Inputs {0} / {1} / {2} / {3}", cellPos, worldPos, mousePosition, touchedTile));

        // register which cell is clicked
        _selectedCell = cellPos;
        Debug.Log("clicked screen location: " + mousePosition + " / clicked cell: " + cellPos);


        Tilemap groundMap = Controller.Battlefield().GroundTilemap();
        List<Unit> units = Controller.Battlefield().Units();

        TileBase tile = groundMap.GetTile(cellPos);
        WorldTile worldTile = null;
        if (tile is WorldTile)
        {
            worldTile = tile as WorldTile;
            Debug.Log(worldTile.ToString());
        }

        if (worldTile != null)
        {
            // check if one of the unit has been selected
            bool newSelectedUnit = false;
            Vector3Int unitCellPos;
            foreach (Unit unit in units)
            {
                unitCellPos = groundMap.WorldToCell(unit.gameObject.transform.position);

                if (cellPos.Equals(unitCellPos))
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
                Vector3 unitNewWorldPos = groundMap.CellToWorld(cellPos);
                unitNewWorldPos.y += 0.25f;
                _selectedUnit.transform.position = unitNewWorldPos;
                Debug.Log(string.Format("{0} unit displaced to {1}", _selectedUnit.name, _selectedCell));
            }
        }
    }

    
}
