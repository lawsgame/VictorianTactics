using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplaceCommand : BattleCommand
{
    private Tilemap _groundmap;
    private Vector3Int _oldCellPos;
    private Vector3Int _newCellPos;
    private Unit _selectedUnit;

    public DisplaceCommand(Tilemap map, Unit selectedUnit, Vector3Int newCellPos)
    {
        _groundmap = map;
        _selectedUnit = selectedUnit;
        _oldCellPos = _groundmap.WorldToCell(selectedUnit.Transform.position);
        _newCellPos = newCellPos;
    }

    protected override void Apply()
    {
        Vector3 unitNewWorldPos = _groundmap.CellToWorld(_newCellPos);
        unitNewWorldPos.y += 0.25f;
        _selectedUnit.transform.position = unitNewWorldPos;
        Debug.Log(string.Format("{0} unit moved to {1}", _selectedUnit.name, _newCellPos));
    }

    protected override void Unapply()
    {
        Vector3 unitOldWorldPos = _groundmap.CellToWorld(_oldCellPos);
        unitOldWorldPos.y += 0.25f;
        _selectedUnit.transform.position = unitOldWorldPos;
        Debug.Log(string.Format("{0} unit moved back at {1}", _selectedUnit.name, _newCellPos));
    }

    public override bool IsExecutable()
    {
        return !Done;
    }

    public override bool IsUndoable()
    {
        return Done;
    }
}
