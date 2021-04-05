using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericArea : IAreaModel
{
    private readonly List<Vector3Int> cellposs;
    private readonly Battle battle;

    public GenericArea(Battle battle, List<Vector3Int> cellposs)
    {
        this.battle = battle;
        this.cellposs = cellposs;
    }

    public List<Vector3Int> GetCells() => cellposs;

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> tiles = new List<WorldTile>();
        foreach(Vector3Int cellpos in cellposs)
        {
            if (battle.Battlefield.HasTile(cellpos))
            {
                tiles.Add(battle.Battlefield.GetTile<WorldTile>(cellpos));
            }
        }
        return tiles;
    }

    public bool IsInside(Vector3Int target) => cellposs.Contains(target);
}
