
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TMath;

public class ConcentricArea : IAreaModel
{
    public Tilemap Map { get; set; }
    public int RangeMin { get; set; }
    public int RangeMax { get; set; }
    public Vector3Int Center { get; set; }

    private bool _full;

    public ConcentricArea(Tilemap map)
    {
        this.Map = map;
        this.RangeMin = 0;
        this.RangeMax = 0;
        this.Center = Vector3Int.zero;
        this._full = false;
    }

    public ConcentricArea(Tilemap map, Vector3Int center, int rangeMin, int rangeMax, bool full)
    {
        this.Map = map;
        this.Center = center;
        this.RangeMax = rangeMax;
        this.RangeMin = rangeMin;
        this._full = full;
    }

    public List<Vector3Int> GetCells()
    {
        List<Vector3Int> area = new List<Vector3Int>();
        Vector3Int gPos;
        for (int xx = Center.x - RangeMax; xx <= Center.x + RangeMax; xx++ ){
            for (int yy = Center.y - RangeMax; yy <= Center.y + RangeMax; yy++)
            {
                gPos = new Vector3Int(xx, yy, 0);
                if (IsInside(gPos) && Map.HasTile(gPos)) 
                    area.Add(gPos);
            }
        }
        return area;
    }

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> area = new List<WorldTile>();
        Vector3Int gPos;
        for (int xx = Center.x - RangeMax; xx <= Center.x + RangeMax; xx++)
        {
            for (int yy = Center.y - RangeMax; yy <= Center.y + RangeMax; yy++)
            {
                gPos = new Vector3Int(xx, yy, 0);
                if (IsInside(gPos) && Map.HasTile(gPos))
                    area.Add(Map.GetTile<WorldTile>(gPos));
            }
        }
        return area;
    }

    public bool IsInside(Vector3Int target) => IsInside(target.x, target.y);

    private bool IsInside(int targetX, int targetY)
    {
        int distX = Math.Abs(targetX - Center.x);
        int distY = Math.Abs(targetY - Center.y);
        if (_full)
            return RangeMin <= Math.Max(distX, distY) && Math.Max(distX, distY) <= RangeMax;
        else
            return RangeMin <= distX + distY && distX + distY <= RangeMax;
    }


}
