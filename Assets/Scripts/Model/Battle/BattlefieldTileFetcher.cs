using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattlefieldTileFetcher
{
    private Tilemap map;

    public Tilemap Map => map;

    private BattlefieldTileFetcher()
    {
    }

    public static BattlefieldTileFetcher Create(Tilemap map)
    {
        BattlefieldTileFetcher instance = new BattlefieldTileFetcher();
        instance.map = map;
        return instance;
    }

    //***$$$ TILE MATRIX

    public struct TileMatrix
    {
        public int minX;
        public int minY;
        public int z;
        public WorldTile[,] tiles;

        public TileMatrix(BoundsInt bounds, int z)
        {
            minX = bounds.min.x;
            minY = bounds.min.y;
            this.z = z;
            tiles = new WorldTile[bounds.max.x - bounds.min.x + 1, bounds.max.y - bounds.min.y + 1];
        }

        public bool HasTile(int i, int j)
        {
            return tiles[i, j] != null;
        }

        public Vector3Int Get(int i, int j, out WorldTile tile)
        {
            tile = tiles[i, j];
            return new Vector3Int(minX + i, minY + j, z);
        }

    }

    public struct WorldTileWrapper
    {
        public Vector3Int position;
        public WorldTile tile;

        public WorldTileWrapper(Vector3Int position, WorldTile tile)
        {
            this.position = position;
            this.tile = tile;
        }
    }


    public TileMatrix GetMapAsTileMatrix()
    {
        map.CompressBounds();
        BoundsInt worldBounds = map.cellBounds;
        TileMatrix matrix = new TileMatrix(worldBounds, 0);
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (map.HasTile(gridPosition))
                {
                    WorldTile tile = map.GetTile(gridPosition) as WorldTile;
                    matrix.tiles[i - worldBounds.min.x, j - worldBounds.min.y] = tile;
                }
            }
        }
        return matrix;
    }


    public List<WorldTileWrapper> GetMapAsTileList()
    {
        map.CompressBounds();
        BoundsInt worldBounds = map.cellBounds;
        List<WorldTileWrapper> tileList = new List<WorldTileWrapper>();
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (map.HasTile(gridPosition))
                {
                    WorldTile tile = map.GetTile(gridPosition) as WorldTile;
                    tileList.Add(new WorldTileWrapper(gridPosition, tile));
                }
            }
        }
        return tileList;
    }
}
