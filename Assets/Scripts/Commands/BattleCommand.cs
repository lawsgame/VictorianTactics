using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : Command
{
    private bool _done = false;

    public bool Done => _done;

    public void execute()
    {
        if (!_done)
        {
            _done = true;
            Apply();
        }
        else Debug.LogWarningFormat("Command ({0}) already done, apply is aborted", this);
    }

    public void undo()
    {
        if (_done)
        {
            _done = false;
            Unapply();
        }
        else Debug.LogWarningFormat("Command ({0}) not executed, no unapply to do", this);
    }

    public abstract bool IsUndoable();
    public abstract bool IsExecutable();
    public abstract void Apply();
    public abstract void Unapply();

}
