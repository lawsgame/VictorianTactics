using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IAreaModel
{
    List<WorldTile> GetTiles();
    List<Vector3Int> GetCells();
    bool IsInside(Vector3Int target);
}
