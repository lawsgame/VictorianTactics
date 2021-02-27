using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System.Linq;

public class UnitGroup : IUnitGroup
{
    private Tilemap _groundmap;
    private Dictionary<Vector3Int, Unit> _units;

    public UnitGroup(Tilemap groundmap)
    {
        _groundmap = groundmap;
        _units = new Dictionary<Vector3Int, Unit>();
    }


    public Unit GetUnitFrom(Vector3Int mapPos) => (_units.ContainsKey(mapPos)) ? _units[mapPos] : null;
    public Vector3Int GetMapPos(Unit unit) => _groundmap.WorldToCell(unit.transform.position);
    public List<Unit> GetUnits() => GetUnits(true);
    public bool IsTileOccupied(Vector3Int mapPos) => _units.ContainsKey(mapPos);

    public List<Unit> GetUnits(bool activeOnly)
    {
        List<Unit> activeUnits = new List<Unit>();
        foreach (Unit u in _units.Values)
        {
            if (u.gameObject.activeInHierarchy || !activeOnly)
            {
                activeUnits.Add(u);
            }
        }
        return activeUnits;
    }

    public void Add(Unit unit)
    {
        Vector3Int unitPos = GetMapPos(unit);
        if (IsTileOccupied(unitPos))
            Debug.LogErrorFormat("Add ({0}) at {1} aborted, tile is already occupied by {2}", unit, unitPos, _units[unitPos]);
        if (!_units.ContainsValue(unit))
            _units[unitPos] = unit;
    }

    public void Move(Unit unitInput, Vector3Int oldUnitPos, Vector3Int newUnitPos)
    {
        if (!IsTileOccupied(oldUnitPos))
            Debug.LogErrorFormat("({0}) not found at its expected initial position {1}",unitInput, oldUnitPos);
        Unit storedUnitAtOldPos = GetUnitFrom(oldUnitPos);
        if (!storedUnitAtOldPos.Equals(unitInput))
            Debug.LogErrorFormat("Unit expected at {0} is ({1}) but found ({2})", oldUnitPos, unitInput, storedUnitAtOldPos);
        if (IsTileOccupied(newUnitPos))
        {
            Unit storedUnitAtNewPos = GetUnitFrom(oldUnitPos);
            Debug.LogErrorFormat("Unit found ({0}) at the destination {1} of ({2}) coming from  {3}", storedUnitAtNewPos,newUnitPos, unitInput, oldUnitPos);
        }
        
        _units.Remove(oldUnitPos);
        _units[newUnitPos] = unitInput;
    }

    public void Switch(Vector3Int unit1Pos, Vector3Int unit2Pos)
    {
        Unit unit1 = GetUnitFrom(unit1Pos);
        Unit unit2 = GetUnitFrom(unit2Pos);
        if (unit1 == null)
            Debug.LogErrorFormat("No unit at {1}", unit1Pos);
        if (unit2 == null)
            Debug.LogErrorFormat("No unit at {1}", unit2Pos);

        _units[unit1Pos] = unit2;
        _units[unit2Pos] = unit1;
    }

    public override string ToString()
    {
        var unitList = _units.Select(p => string.Format("{0} at {1} ", p.Value, p.Key));
        return string.Join(" | ", unitList);
    }
}
