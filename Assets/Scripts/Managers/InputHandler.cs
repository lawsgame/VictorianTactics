﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class InputHandler : MonoBehaviour
{

    [SerializeField] private float _sensivityTime = 0.1f;

    private float _delay = 0;
    private bool _pressed = false;
    private bool _longPressed = false;
    private Vector2 _previousMousePos;


    void Update()
    {

        if (_pressed)
        {
            _delay += Time.deltaTime;
            if (!_longPressed && _delay > _sensivityTime)
            {
                _longPressed = true;
                _previousMousePos = Input.mousePosition;
            }
                
        }

        if (Input.GetMouseButtonDown(0))
        {
            _delay = 0;
            _pressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_pressed && !_longPressed)
                OnTouch(Input.mousePosition);
            _pressed = false;
            _longPressed = false;
        }

        if (_longPressed)
        {
            OnPan(Input.mousePosition, _previousMousePos);
            _previousMousePos = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonDown(2))
            OnLongTouch(Input.mousePosition);
    }

    public abstract void OnTouch(Vector2 mousePosition);
    public abstract void OnLongTouch(Vector2 mousePosition);
    public abstract void OnPan(Vector2 mousePosition, Vector2 mousePreviousPos);

    
}