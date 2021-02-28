using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommand : Command
{
    private bool _done = false;

    public bool Done => _done;

    public void Execute()
    {
        if (!_done)
        {
            _done = true;
            Apply();
        }
        else Debug.LogWarningFormat("Command ({0}) already done, apply is aborted", this);
    }

    public void Undo()
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
    protected abstract void Apply();
    protected abstract void Unapply();

}
