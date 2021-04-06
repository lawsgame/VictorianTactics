using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static BattlefieldTileFetcher;

public class BattlefieldDecorator : MonoBehaviour
{
    private Tilemap map;
    private BattlefieldTileFetcher tileFetcher;
    private Dictionary<Vector3Int, GameObject> decorations;

    private void Awake()
    {
        map = GetComponent<Tilemap>();
        tileFetcher = BattlefieldTileFetcher.Create(map);
        decorations = new Dictionary<Vector3Int, GameObject>();
    }

    public void Decorate()
    {
        List<WorldTileWrapper> worldMap = tileFetcher.GetMapAsTileList();
        foreach (WorldTileWrapper tileWrapper in worldMap)
            UpdateDecorationIfAny(tileWrapper.position, tileWrapper.tile);
    }

    public void UpdateDecorationIfAny(Vector3Int tilePos, WorldTile worldTile)
    {
        RemoveDecorationIfAny(tilePos);

        if (worldTile.HasDecoration)
        {
            Vector3 tileWorldPosition = map.CellToWorld(tilePos);
            GameObject decoration = GameObject.Instantiate(worldTile.decoration, tileWorldPosition, Quaternion.identity);
            decoration.transform.SetParent(map.gameObject.transform);
            decorations.Add(tilePos, decoration);
        }
    }

    public void RemoveDecorationIfAny(Vector3Int mappos)
    {
        if (decorations.ContainsKey(mappos))
        {
            GameObject decoration = decorations[mappos];
            decorations.Remove(mappos);
            GameObject.Destroy(decoration);
        }
    }
}
