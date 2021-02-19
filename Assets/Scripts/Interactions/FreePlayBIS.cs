using UnityEngine;
using UnityEngine.Tilemaps;

public class FreePlayBIS : BattleInterractionState
{
    private Tilemap _groundTilemap;
    private GameObject _selectedUnit;
    private Vector3Int _selectedCell;

    public FreePlayBIS(BattleInterractionMachine machine, Tilemap groundTilemap) : base(machine)
    {
        _groundTilemap = groundTilemap;
    }

    public override void Dispose()
    {
        Debug.Log("Dispode of FreePlayBIS");
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

        // find all units
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        
        // check if one of the unit has been selected
        bool newSelectedUnit = false;
        Vector3Int unitCellPos;
        foreach (GameObject unit in units) {
            unitCellPos = _groundTilemap.WorldToCell(unit.transform.position);
            Debug.Log("["+unit.name+"] unit pos: " + unit.transform.position+" / unit cell pos: " + unitCellPos);
   
            if (clickCellPos.Equals(unitCellPos))
            {
                newSelectedUnit = true;
                _selectedUnit = unit;
                Debug.Log("New selected unit: " + unit.name);
                break;
            }
        }

        // if an empty tile has been selected, move the selected unit to this tile
        if (!newSelectedUnit && _selectedUnit != null)
        {
            Vector3 unitNewWorldPos = _groundTilemap.CellToWorld(clickCellPos);
            unitNewWorldPos.y += 0.25f;
            _selectedUnit.transform.position = unitNewWorldPos;
            Debug.Log(string.Format("{0} unit displaced to {1}", _selectedUnit.name, _selectedCell));
        }
    }

    public override void Init()
    {
        Debug.Log("End InitBIS");
    }
}
