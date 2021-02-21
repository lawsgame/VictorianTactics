using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using static Battlefield;

public class ContextMenuItemLibrary 
{

    [MenuItem("CONTEXT/Transform/Center On Closest Cell")]
    static void CenterOnCell(MenuCommand command)
    {

        Transform t = command.context as Transform;
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        Tilemap groundTilemap = ground.GetComponent<Tilemap>();
        Vector3Int cellPos = groundTilemap.WorldToCell(t.position);
        Vector3 ajustedWorldPos = groundTilemap.CellToWorld(cellPos);
        ajustedWorldPos.y += 0.25f;
        t.position = ajustedWorldPos;
        
    }

    [MenuItem("CONTEXT/Transform/Main Test")]
    static void MainTest(MenuCommand command)
    {

        GameObject go = GameObject.FindGameObjectWithTag("Ground");
        Tilemap groundTilemap = go.GetComponent<Tilemap>();
        Debug.Log("cell bounds: "+groundTilemap.cellBounds);
/*
        Battlefield.TileMatrix m = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battlefield>().GetMapAsTileMatrix();

        Debug.Log(m.minX);
        Debug.Log(m.minY);
        Debug.Log(m.z);
        for (int i = 0; i < m.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < m.tiles.GetLength(1); j++)
            {
                WorldTile tile = null;
                Vector3Int gridpos = m.Get(i, j, out tile);
                if (tile != null)
                {
                    Debug.Log(string.Format("tile {0} at pos {1}", tile, gridpos));
                }
            }
        }*/

        List<WorldTileWrapper> lt = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battlefield>().GetMapAsTileList();
        foreach(WorldTileWrapper t in lt){
            
            Debug.Log(string.Format("tile {0} at pos {1}", t.position, t.tile));
        }
    }

}
