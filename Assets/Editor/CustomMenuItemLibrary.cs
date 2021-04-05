using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using static Battle;

public class MenuItemLibrary 
{

    //*** TESTS
    
    [MenuItem("CONTEXT/Transform/OutTest")]
    static void CurrentTest(MenuCommand command)
    {

        
        
    }

    [MenuItem("CONTEXT/Transform/PlayTest")]
    static void PLayTest(MenuCommand command)
    {

        GameObject go = GameObject.FindGameObjectWithTag("Ground");
        Battle battle = GameObject.FindGameObjectWithTag("BattleField").GetComponent<Battle>();
        Tilemap groundTilemap = go.GetComponent<Tilemap>();

        Debug.Log(battle.PartyRecorder.AreOpposed(0, 0));
        Debug.Log(battle.PartyRecorder.AreOpposed(0, 1));
        Debug.Log(battle.PartyRecorder.AreOpposed(1, 0));
        Debug.Log(battle.PartyRecorder.AreOpposed(1, 1));

    }
}
