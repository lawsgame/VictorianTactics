
public interface State 
{
    void Init();    // to be called whenever the state is activated
    void End();     // to be called whenever the state is disactivated
    void Dispose(); //to be called whener the state is removed from the stack
}
