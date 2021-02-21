using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldTileModel;

[CreateAssetMenu()]
public class WorldTile : Tile
{

    [SerializeField] private GameObject decoration;
    [SerializeField] private WorldTileType type;


    public WorldTileType Type { get => type;  }
    public WorldTileModel Model { get => WorldTileModel.Find(type);  }


    public override string ToString()
    {
        return string.Format("Tile({0}) ", type);
    }
}
