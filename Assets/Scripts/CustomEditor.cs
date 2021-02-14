using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditor 
{
    [MenuItem("CONTEXT/Transform/Uniform normalized scale")]
    static void ScaleUniform(MenuCommand command)
    {
        Transform t = command.context as Transform;
        t.localScale = Vector3.one;
    }

    [MenuItem("CONTEXT/Transform/Main Test")]
    static void TestMain(MenuCommand command)
    {
        
    }

    
}
