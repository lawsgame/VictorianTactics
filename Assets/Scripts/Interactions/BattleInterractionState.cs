using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleInterractionState: State
{
    protected BattleController Controller { get; }

    public BattleInterractionState(BattleController controller)
    {
        Controller = controller;
    }

    public abstract void Init();
    public abstract void End();
    public abstract void Dispose();

    public abstract void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePos, WorldTile touchedTile);
    public abstract void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePos, WorldTile touchedTile);
    public abstract void OnPan(Vector2 worldPos, Vector2 mousePos, Vector2 mouseDl, Vector2 worldDl);
    

}
