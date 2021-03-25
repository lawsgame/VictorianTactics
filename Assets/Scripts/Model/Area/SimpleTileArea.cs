using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleTileArea : IAreaModel
{
    private Tilemap _ground;
    private Vector3Int _tilepos;

    public SimpleTileArea(Tilemap ground, Vector3Int tilepos)
    {
        this._ground = ground;
        this._tilepos = tilepos;
    }

    public List<Vector3Int> GetCells() {
        List<Vector3Int> cellpos = new List<Vector3Int>();
        cellpos.Add(_tilepos);
        return cellpos;
     }

    public List<WorldTile> GetTiles()
    {
        List<WorldTile> tiles = new List<WorldTile>();
        tiles.Add(_ground.GetTile<WorldTile>(_tilepos));
        return tiles;
    }

    public bool IsInside(Vector3Int target) => target.Equals(_tilepos);
}
