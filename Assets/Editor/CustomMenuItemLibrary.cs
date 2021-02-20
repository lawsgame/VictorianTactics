using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

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

        GameObject groundObject = GameObject.FindGameObjectWithTag("Ground");
        Tilemap groundTilemap = groundObject.GetComponent<Tilemap>();
        groundTilemap.CompressBounds();
        Bounds mapbounds = groundTilemap.localBounds;
        Debug.Log("Min World Bound: "+mapbounds.min);
        Debug.Log("Max World Bound: " + mapbounds.max);

        TileBase[] tiles = groundTilemap.GetTilesBlock(new BoundsInt(new Vector3Int(-2, -2, 0), new Vector3Int(2, 2, 0)));
        Debug.Log("number of tiles found: "+tiles.Length);
        

    }

}
