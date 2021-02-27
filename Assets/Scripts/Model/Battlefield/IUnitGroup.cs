using System.Collections.Generic;
using UnityEngine;

public interface IUnitGroup
{
    bool IsTileOccupied(Vector3Int mapPos);
    void Add(Unit unit);
    void Move(Unit unit, Vector3Int oldUnitPos, Vector3Int newUnitPos);
    void Switch(Vector3Int unit1Pos, Vector3Int unit2Pos);
    Vector3Int GetMapPos(Unit unit);
    Unit GetUnitFrom(Vector3Int mapPos);
    List<Unit> GetUnits();
    List<Unit> GetUnits(bool activeOnly);
}