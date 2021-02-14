using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayBIS : BattleInterractionState
{
    public override void Dispose()
    {
        Debug.Log("Dispode of FreePlayBIS");
    }

    public override void End()
    {
        Debug.Log("End FreePlayBIS");
    }

    public override void handleInput(Vector3Int cellPos, Vector2 MousePosition)
    {
        Debug.Log("clicked cell: "+cellPos);
        Debug.Log("clicked screen location: " + MousePosition);
    }

    public override void Init()
    {
        Debug.Log("End InitBIS");
    }
}
