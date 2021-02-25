using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : Command
{
    public abstract bool IsExecutable();
    public abstract bool IsUndoable();
    public abstract void execute();
    public abstract void undo();


    public abstract class ActorCommand: BattleCommand
    {

    }
}
