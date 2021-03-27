using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(Unit))]
public class UnitEditor : Editor
{

    //private static readonly string label_CenterUnitsOnClosestCell = "Center Unit on Closest Cell";
    //private static readonly string label_ResyncOrientation = "Resynchronize orientation";

    private static readonly string button_CenterUnitsOnClosestCell = "Center On Cell";
    private static readonly string button_ResyncOrientation = "Resynchronize Orientation";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Unit unit = target as Unit;

        CenterOnClosestCell(unit);
        ResyncOrientation(unit);
    }


    private void CenterOnClosestCell(Unit unit)
    {
        //GUILayout.Label(label_CenterUnitsOnClosestCell);
        if (GUILayout.Button(button_CenterUnitsOnClosestCell))
        {
            
            Transform t = unit.transform;
            GameObject ground = GameObject.FindGameObjectWithTag("Ground");
            Tilemap groundTilemap = ground.GetComponent<Tilemap>();
            Vector3Int cellPos = groundTilemap.WorldToCell(t.position);
            Vector3 ajustedWorldPos = groundTilemap.CellToWorld(cellPos);
            ajustedWorldPos.y += 0.25f;
            t.position = ajustedWorldPos;
        }
    }

    private void ResyncOrientation(Unit unit)
    {
        //GUILayout.Label(label_ResyncOrientation);
        if (GUILayout.Button(button_ResyncOrientation))
        {
            unit.Scheduler.QueueNext(UnitAnimation.Key.Idle, unit.Model.orientation);
        }
    }

}
