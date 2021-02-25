using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleInterractionState:State
{
    protected BattleController Controller { get; }

    public BattleInterractionState(BattleController machine)
    {
        Controller = machine;
    }

    public abstract void Init();
    public abstract void End();
    public abstract void Dispose();


    public abstract void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile);
    public abstract void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 MousePosition, WorldTile touchedTile);
    public abstract void Pan(Vector2 worldPos, Vector2 MousePosition);
    public abstract void OnKeyPressed(KeyCode keyCode);

}
