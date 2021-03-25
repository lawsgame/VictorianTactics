using System;
using UnityEngine;

public static class TMath 
{
    public static int Dist(Vector3Int gridPos1, Vector3Int gridPos2) => Math.Abs(gridPos1.x - gridPos2.x) + Math.Abs(gridPos1.y - gridPos2.y);
}
