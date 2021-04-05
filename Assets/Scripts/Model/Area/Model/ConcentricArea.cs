
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TMath;

public class ConcentricArea : IAreaModel
{
    private readonly Tilemap _ground;
    private readonly int _rangeMin;
    private readonly int _rangeMax;
    private readonly bool _full;
    private readonly Vector3Int _center;

    public ConcentricArea(Tilemap ground, Vector3Int center, int rangeMin, int rangeMax, bool full)
    {
        this._ground = ground;
        this._center = center;
        this._rangeMax = rangeMax;
        this._rangeMin = rangeMin;
        this._full = full;
    }

    public List<Vector3Int> GetCells()
    {
        List<Vector3Int> area = new List<Vector3Int>();
        Vector3Int gPos;
        for (int xx = _center.x - _rangeMax; xx <= _center.x + _rangeMax; xx++ ){
            for (int yy = _center.y - _rangeMax; yy <= _center.y + _rangeMax; yy++)
            {
                gPos = new Vector3Int(xx, yy, 0);
                if (IsInside(gPos) && _ground.HasTile(gPos)) 
                    area.Add(gPos);
            }
        }
        return area;
    }

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> area = new List<WorldTile>();
        Vector3Int gPos;
        for (int xx = _center.x - _rangeMax; xx <= _center.x + _rangeMax; xx++)
        {
            for (int yy = _center.y - _rangeMax; yy <= _center.y + _rangeMax; yy++)
            {
                gPos = new Vector3Int(xx, yy, 0);
                if (IsInside(gPos) && _ground.HasTile(gPos))
                    area.Add(_ground.GetTile<WorldTile>(gPos));
            }
        }
        return area;
    }

    public bool IsInside(Vector3Int target) => IsInside(target.x, target.y);

    private bool IsInside(int targetX, int targetY)
    {
        int distX = Math.Abs(targetX - _center.x);
        int distY = Math.Abs(targetY - _center.y);
        if (_full)
            return _rangeMin <= Math.Max(distX, distY) && Math.Max(distX, distY) <= _rangeMax;
        else
            return _rangeMin <= distX + distY && distX + distY <= _rangeMax;
    }


}
