using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class InputHandler : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnTouch(Input.mousePosition); 

        if (Input.GetMouseButtonDown(2))
            OnLongTouch(Input.mousePosition);
    }

    public abstract void OnTouch(Vector2 mousePosition);
    public abstract void OnLongTouch(Vector2 mousePosition);
    public abstract void OnPan(Vector2 mousePosition, Vector2 mouseDl);
}
