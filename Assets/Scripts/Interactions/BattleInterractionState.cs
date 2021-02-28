using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleInterractionState: State
{
    private Stack<BattleCommand> _commandHistoric;
    private BattleController _controller;

    protected BattleController Controller => _controller;

    public BattleInterractionState(BattleController controller)
    {
        _controller = controller;
        _commandHistoric = new Stack<BattleCommand>();
    }

    public abstract void Init();
    public abstract void End();
    public abstract void Dispose();

    public abstract void OnTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePos, WorldTile touchedTile);
    public abstract void OnLongTouch(Vector3Int cellPos, Vector2 worldPos, Vector2 mousePos, WorldTile touchedTile);
    public abstract void OnDoubleTap(Vector3Int tilePos, Vector2 worldPos, Vector2 mousePos, WorldTile worldTile);
    public abstract void OnPan(Vector2 worldPos, Vector2 mousePos, Vector2 mouseDl, Vector2 worldDl);

    public bool CommandHistoryEmpty() => _commandHistoric.Count == 0;


    public bool DoCommand(BattleCommand cmd)
    {
        if (cmd.IsExecutable())
        {
            cmd.Execute();
            _commandHistoric.Push(cmd);
            return true;
        }
        return false;
    }


    public bool UndoLastCommand()
    {
        
        if (!CommandHistoryEmpty() && _commandHistoric.Peek().IsUndoable())
        {
            BattleCommand cmd = _commandHistoric.Pop();
            cmd.Undo();
        }
        return false;
    }

}
