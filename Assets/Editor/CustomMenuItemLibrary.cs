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
        Battlefield bf = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battlefield>();
        Tilemap groundTilemap = go.GetComponent<Tilemap>();

        List<Unit> units = bf.Units();
        foreach(Unit u in units)
        {
            //Debug.Log(string.Format(" {0} with HP [1} and STR {2} ", u.gameObject.name, u.Model().HitPoints(), u.Model().Strength()));
            Debug.Log(string.Format(" {0} with HP {1} and STR {2} ", u.gameObject.name, u.Model().HitPoints(), u.Model().Strength()));
        }
        
    }

}
