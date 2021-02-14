using System.Collections;
using System.Collections.Generic;

public interface  Command
{
    void execute ();
    void undo();
}
