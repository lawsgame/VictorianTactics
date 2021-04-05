
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class CrossArea : IAreaModel
{
    private readonly Tilemap _ground;
    private readonly int _rangeMin;
    private readonly int _rangeMax;
    private readonly Vector3Int _center;

    public CrossArea(Tilemap ground, Vector3Int center, int rangeMin, int rangeMax)
    {
        this._rangeMin = rangeMin;
        this._rangeMax = rangeMax;
        this._ground = ground;
        this._center = center;
    }

    public List<Vector3Int> GetCells()
    {
        List<Vector3Int> posList = new List<Vector3Int>();
        Vector3Int cellpos;
        for (int x = _rangeMax + _center.x; x <= _center.x + _rangeMax; x++)
        {
            cellpos = new Vector3Int(x, _center.y, _center.z);
            if (IsInside(cellpos) && _ground.HasTile(cellpos))
                posList.Add(cellpos);
        }
        for(int y = _center.y + _rangeMax; y <= _center.y + _rangeMax; y++)
        {
            cellpos = new Vector3Int(_center.x, y, _center.z);
            if (IsInside(cellpos) && _ground.HasTile(cellpos) )
                posList.Add(cellpos);
        }
        return posList;
    }

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> posList = new List<WorldTile>();
        Vector3Int cellpos;
        WorldTile worldTile;
        for (int x = _rangeMax - _center.x; x <= _center.x + _rangeMax; x++)
        {
            cellpos = new Vector3Int(x, _center.y, 0);
            worldTile = _ground.GetTile<WorldTile>(cellpos);
            if (IsInside(cellpos) && _ground.HasTile(cellpos))
                posList.Add(worldTile);
        }
        for (int y = _center.y - _rangeMax; y <= _center.y + _rangeMax; y++)
        {
            cellpos = new Vector3Int(_center.x, y, 0);
            worldTile = _ground.GetTile<WorldTile>(cellpos);
            if (IsInside(cellpos) && _ground.HasTile(cellpos) && !(y == _center.y))
                posList.Add(worldTile);
        }
        return posList;
    }

    public bool IsInside(Vector3Int target)
    {
        bool inside = false;
        if(target.x == _center.x)
        {
            int distY = Math.Abs(target.y - _center.y);
            inside = _rangeMin <= distY && distY <= _rangeMax;
        }
        else if (target.y == _center.y)
        {
            int distX = Math.Abs(target.x - _center.x);
            inside = _rangeMin <= distX && distX <= _rangeMax;
        }
             
        return inside;
    }
}
