using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleInterractionState:State
{
    protected BattleInterractionMachine Machine { get; }

    public BattleInterractionState(BattleInterractionMachine machine)
    {
        Machine = machine;
    }

    public abstract void Init();
    public abstract void End();
    public abstract void Dispose();


    public abstract void handleInput(Vector3Int cellPos, Vector2 MousePosition);
    
}
