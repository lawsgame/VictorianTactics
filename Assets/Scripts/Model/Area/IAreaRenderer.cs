using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class IAreaRenderer : MonoBehaviour
{
    
    public IAreaModel Model { get; set; }
    public AreaType Type { get; set; }

    public abstract void Draw(Tilemap battleifield);
}
