using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using static Battlefield;
using Tactics.Pathfinder;
using System.Linq;

public class MenuItemLibrary 
{

    //*** TESTS
    
    [MenuItem("CONTEXT/Transform/OutTest")]
    static void CurrentTest(MenuCommand command)
    {

        List<int> intlist = new List<int>{ 1,2,3,4,5};
        var newlist = intlist.Select((value, index) => value + index);
        Debug.Log(string.Join(",", newlist));

        Dictionary<string, int> dico = new Dictionary<string, int>();
        dico.Add("first", 1);
        dico.Add("second", 2);
        dico.Add("third", 3);
        var newdoc = dico.Select(pair => (pair.Key.Substring(0,2), pair.Value));
        var docList = dico.Select(pair => string.Format("key {0} => value {1}",pair.Key, pair.Value));
        foreach (var de in docList)
            Debug.Log(de);
        Debug.Log(string.Join(", ", docList));
        
    }

    [MenuItem("CONTEXT/Transform/PlayTest")]
    static void PLayTest(MenuCommand command)
    {

        GameObject go = GameObject.FindGameObjectWithTag("Ground");
        Battlefield bf = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battlefield>();
        Tilemap groundTilemap = go.GetComponent<Tilemap>();
        List<Unit> units = bf.UnitGroup().GetUnits();

    }


    //*** GAME TOOLS 

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


    [MenuItem("CONTEXT/Battlefield/Show UnitGroup")]
    static void ShowUnitGroup(MenuCommand command)
    {
        Battlefield bf = command.context as Battlefield;
        Debug.Log(bf.UnitGroup().ToString());
    }

}
