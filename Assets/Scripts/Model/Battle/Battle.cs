using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public class Battle : MonoBehaviour
{
    public GameObject tilePosIndicator;

    [SerializeField] private int parties;
    [SerializeField] private List<Opponents> opponents;
    [SerializeField] private Tilemap groundmap;
    
    private List<Unit> _units = null;
    private PartyRelationshipRecorder _partyRecorder = null;

    public Tilemap Groundmap => groundmap;
    public List<Unit> Units() => Units(true);
    public PartyRelationshipRecorder PartyRecorder => _partyRecorder;


    /**  BATTLE FIELD COMPONENT INITIALIZATION **/

    private void Awake()
    {
        List<WorldTileWrapper> worldMapTiles = GetMapAsTileList();
        SetMapDecorations(worldMapTiles);

        _partyRecorder = PartyRelationshipRecorder.create(parties, opponents);
    }

    private void SetMapDecorations(List<WorldTileWrapper> worldMap)
    {
        Vector3 tileWorldPosition;
        GameObject decoration;
        foreach(WorldTileWrapper tileWrapper in worldMap)
        {
            if (tileWrapper.tile.HasDecoration)
            {
                tileWorldPosition = groundmap.CellToWorld(tileWrapper.position);
                decoration = Instantiate(tileWrapper.tile.decoration, tileWorldPosition, Quaternion.identity);
                decoration.transform.SetParent(this.gameObject.transform);
                
            }
        }
    }

    //*** UNIT MANAGEMENT

    public void AddUnit(Unit unit)
    {
        if(_units == null)
            _units = new List<Unit>();
        _units.Add(unit);
    }

    public List<Unit> Units(bool activeOnly)
    {
        List<Unit> activeUnits = new List<Unit>();
        foreach (Unit u in _units)
        {
            if (u.gameObject.activeInHierarchy || !activeOnly)
            {
                activeUnits.Add(u);
            }
        }
        return activeUnits;
    }

    public Unit GetUnitFrom(Vector3Int mapPos)
    {
        Unit found = null;
        Vector3Int unitPos;
        foreach (Unit u in _units)
        {
            if (u.gameObject.activeInHierarchy)
            {
                unitPos = groundmap.WorldToCell(u.Transform.position);
                if (unitPos.Equals(mapPos))
                {
                    found = u;
                    break;
                }
            }
        }
        return found;
    }

    public bool IsTileOccupied(Vector3Int mapPos)
    {
        return GetUnitFrom(mapPos) == null;
    }

    //*** TILE MATRIX

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
        groundmap.CompressBounds();
        BoundsInt worldBounds = groundmap.cellBounds;
        TileMatrix matrix = new TileMatrix(worldBounds, 0);
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (groundmap.HasTile(gridPosition))
                {
                    WorldTile tile = groundmap.GetTile(gridPosition) as WorldTile;
                    matrix.tiles[i - worldBounds.min.x, j - worldBounds.min.y] = tile;
                }
            }
        }
        return matrix;
    }


    public List<WorldTileWrapper> GetMapAsTileList()
    {
        groundmap.CompressBounds();
        BoundsInt worldBounds = groundmap.cellBounds;
        List<WorldTileWrapper> tileList = new List<WorldTileWrapper>();
        Vector3Int gridPosition;
        for (int i = worldBounds.min.x; i <= worldBounds.max.x; i++)
        {
            for (int j = worldBounds.min.y; j <= worldBounds.max.y; j++)
            {
                gridPosition = new Vector3Int(i, j, 0);
                if (groundmap.HasTile(gridPosition))
                {
                    WorldTile tile = groundmap.GetTile(gridPosition) as WorldTile;
                    tileList.Add(new WorldTileWrapper(gridPosition, tile));
                }
            }
        }
        return tileList;
    }
}
