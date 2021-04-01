using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using static Battle;
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
        Battle bf = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battle>();
        Tilemap groundTilemap = go.GetComponent<Tilemap>();

    }
}
