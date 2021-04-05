using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TraversableArea : IAreaModel
{
    private readonly Battle _battle;
    private readonly Unit _walker;


    public TraversableArea(Battle battle, Unit walker)
    {
        this._battle = battle;
        this._walker = walker;
    }

    public List<Vector3Int> GetCells() => TraversableAreaFinder.Algorithm.GetTravesableArea(_battle, _walker);

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> worldTiles = new List<WorldTile>();
        List<Vector3Int> areaGridPos = GetCells();
        foreach (Vector3Int gridpos in areaGridPos)
        {
            worldTiles.Add(_battle.Battlefield.GetTile<WorldTile>(gridpos));
        }
        return worldTiles;
    }

    public bool IsInside(Vector3Int target)
    {
        List<Vector3Int> areaGridPos = GetCells();
        return areaGridPos.Contains(target);
    }

}
