using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using static Battlefield;
using Tactics.Pathfinder;

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


        Node node1 = new Node(null, Vector3Int.one,     1,4) ;
        Node node2 = new Node(null, Vector3Int.one * 2, 0,3) ;
        Node node3 = new Node(null, Vector3Int.one * 3, 5,9);
        Node node4 = new Node(null, Vector3Int.one * 4, 3, 2);



        List<Node> nodes = new List<Node>
        {
            node1,
            node2,
            node3,
            node4
        };

        foreach(Node nn in nodes) Debug.Log(nn.ToLongString());

        Debug.Log(string.Join(", ", nodes));
        Debug.Log("sorting");
        nodes.Sort();
        Debug.Log(string.Join(", ", nodes));

        // test GetPath as well


    }

}
