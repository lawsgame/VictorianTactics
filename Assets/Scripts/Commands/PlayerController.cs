﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject selectedUnit;
    Tilemap floorTilemap;

    void Awake()
    {
        //floorTilemap = GameObject.FindGameObjectWithTag("Floor") as Tilemap;    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

}
