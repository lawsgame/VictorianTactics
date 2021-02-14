using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachine<S> where S : State
{


    Stack<S> _stateStack = new Stack<S>();
     
    public bool IsEmpty() => _stateStack.Count == 0;

    public void Push(S s)
    {
        if (!IsEmpty())
        {
            this._stateStack.Peek().End();
        }
        _stateStack.Push(s);
        _stateStack.Peek().Init();
    }

    public void Rollback()
    {
        if (IsEmpty())
        {
            throw new Exception("rollback on statemachine without fallback state");
        }
        else
        {
            _stateStack.Peek().End();
            _stateStack.Peek().Dispose();
            _stateStack.Pop();
            _stateStack.Peek().Init();
        }
    }

    public void Replace(S s)
    {
        if (!IsEmpty())
        {
            _stateStack.Peek().End();
            _stateStack.Peek().Dispose();
            _stateStack.Pop();
        }
        _stateStack.Push(s);
        _stateStack.Peek().Init();
    }

    public S GetCurrentState()
    {
        return _stateStack.Peek();
    }

}
