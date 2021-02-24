using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static WorldTileTemplate;

public class Battlefield : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;

    private readonly List<Unit> units = new List<Unit>();

    private void Awake()
    {
        SetMapDecorations();
    }

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
            return tiles[i,j] != null;
        } 

        public Vector3Int Get(int i,  int j, out WorldTile tile)
        {
            tile = tiles[i,j];
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



    public Tilemap GroundTilemap() => groundTilemap;
    public void AddUnit(Unit unit) => units.Add(unit);
    public List<Unit> Units() => Units(true);


    public List<Unit> Units(bool activeOnly)
    {
        List<Unit> activeUnits = new List<Unit>();
        foreach(Unit u in units)
        {
            if (u.gameObject.activeInHierarchy || !activeOnly)
            {
                activeUnits.Add(u);
            }
        }
        return activeUnits;
    }


    private void SetMapDecorations()
    {
        List<WorldTileWrapper> tileWrappers = GetMapAsTileList();
        Vector3 tileWorldPosition;
        GameObject decoration;
        foreach(WorldTileWrapper wrapper in tileWrappers)
        {
            if (wrapper.tile.HasDecoration)
            {
                tileWorldPosition = groundTilemap.CellToWorld(wrapper.position);
                decoration = Instantiate(wrapper.tile.decoration, tileWorldPosition, Quaternion.identity);
                decoration.transform.SetParent(this.gameObject.transform);
                
            }
        }
        
    }


    public TileMatrix GetMapAsTileMatrix()
    {
        groundTilemap.CompressBounds();
        BoundsInt worldBounds = groundTilemap.cellBounds;
        TileMatrix matrix = new TileMatrix(worldBounds, 0);
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (groundTilemap.HasTile(gridPosition))
                {
                    WorldTile tile = groundTilemap.GetTile(gridPosition) as WorldTile;
                    matrix.tiles[i - worldBounds.min.x, j - worldBounds.min.y] = tile;
                }
            }
        }
        return matrix;
    }


    public List<WorldTileWrapper> GetMapAsTileList()
    {
        groundTilemap.CompressBounds();
        BoundsInt worldBounds = groundTilemap.cellBounds;
        List<WorldTileWrapper> tileList = new List<WorldTileWrapper>();
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (groundTilemap.HasTile(gridPosition))
                {
                    WorldTile tile = groundTilemap.GetTile(gridPosition) as WorldTile;
                    tileList.Add(new WorldTileWrapper(gridPosition, tile));
                }
            }
        }
        return tileList;
    }
}
