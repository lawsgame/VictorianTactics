using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldTileTemplate;

[CreateAssetMenu()]
public class WorldTile : Tile
{

    public GameObject decoration;

    [SerializeField] private WorldTileType type;


    public WorldTileType Type { get => type;  }
    public WorldTileTemplate Model { get => WorldTileTemplate.Find(type);  }

    public bool HasDecoration { get => decoration != null;  }


    public override string ToString()
    {
        return string.Format("Tile({0}) ", type);
    }
}
