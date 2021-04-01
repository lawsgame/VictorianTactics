using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Battle;

[CustomEditor(typeof(Battle))]
public class BattleEditor : Editor
{
    private static readonly string label_MapCoords = "Map Coords";

    private static readonly string button_MapCoords_show = "Show";
    private static readonly string button_MapCoords_hide = "Hide";


    private static readonly string GPTH = "GridPosTextHolder";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Battle battlefield = target as Battle;

        ShowMapPositionButtons(battlefield);
    }


    private void ShowMapPositionButtons(Battle battlefield)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label_MapCoords);

        if (GUILayout.Button(button_MapCoords_show))
        {

            GameObject textHolder = GameObject.Find(GPTH);
            if (textHolder == null)
            {
                Debug.Log("show text holder");

                textHolder = new GameObject(GPTH);
                textHolder.transform.SetParent(battlefield.gameObject.transform);

                Vector3 tileWorldPosition;
                GameObject tilePosIndicator;
                foreach (WorldTileWrapper wrapper in battlefield.GetMapAsTileList())
                {
                    tileWorldPosition = battlefield.Groundmap.CellToWorld(wrapper.position);
                    tilePosIndicator = Instantiate(battlefield.tilePosIndicator, tileWorldPosition, Quaternion.identity);
                    tilePosIndicator.transform.SetParent(textHolder.gameObject.transform);
                    tilePosIndicator.transform.position += new Vector3(0f, 0.25f, 0f); 
                    TextMeshPro mesh = tilePosIndicator.GetComponent<TextMeshPro>();
                    if (mesh != null)
                    {
                        mesh.text = string.Format("{0},{1}", wrapper.position.x, wrapper.position.y);
                    }
                }
            }

        }

        if (GUILayout.Button(button_MapCoords_hide))
        {
            GameObject textHolder = GameObject.Find(GPTH);
            if (textHolder != null)
            {
                Debug.Log("hide text holder");
                GameObject.DestroyImmediate(textHolder);
            }
        }

        GUILayout.EndHorizontal();
    }
}
