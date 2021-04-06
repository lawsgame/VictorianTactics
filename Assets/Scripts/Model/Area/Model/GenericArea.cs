using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericArea : IAreaModel
{
    private readonly List<Vector3Int> cellposs;
    private readonly Battlefield battlefield;

    public GenericArea(Battlefield battlefield, List<Vector3Int> cellposs)
    {
        this.battlefield = battlefield;
        this.cellposs = cellposs;
    }

    public List<Vector3Int> GetCells() => cellposs;

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> tiles = new List<WorldTile>();
        foreach(Vector3Int cellpos in cellposs)
        {
            if (battlefield.Map.HasTile(cellpos))
            {
                tiles.Add(battlefield.Map.GetTile<WorldTile>(cellpos));
            }
        }
        return tiles;
    }

    public bool IsInside(Vector3Int target) => cellposs.Contains(target);
}
