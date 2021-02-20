using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Battlefield : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    public List<Unit> Units { get; set; } = new List<Unit>();

    public Tilemap GroundTilemap() => groundTilemap;

}
