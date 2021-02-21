using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTileModel
{

    public enum WorldTileType
    {
        Ocean,
        Swamp,
        Swallow,
        DeepRiver,
        Sand,
        Desert,
        Hill,
        Mountain,
        Woods,
        DarkForest,
        Fields,
        LowLand,
        Ruin,
        Village,
        Castle,
        AncientSite,
        Undefined
    }

    private static readonly Dictionary<WorldTileType, WorldTileModel> tileTypes = new Dictionary<WorldTileType, WorldTileModel>();

    static WorldTileModel()
    {
        tileTypes.Add(WorldTileType.Ocean, new WorldTileModel(WorldTileType.Ocean, 1, false, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Swamp, new WorldTileModel(WorldTileType.Swamp, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Swallow, new WorldTileModel(WorldTileType.Swallow, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.DeepRiver, new WorldTileModel(WorldTileType.DeepRiver, 1, false, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Sand, new WorldTileModel(WorldTileType.Sand, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Desert, new WorldTileModel(WorldTileType.Desert, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Hill, new WorldTileModel(WorldTileType.Hill, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Mountain, new WorldTileModel(WorldTileType.Mountain, 1, false, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Woods, new WorldTileModel(WorldTileType.Woods, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.DarkForest, new WorldTileModel(WorldTileType.DarkForest, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Fields, new WorldTileModel(WorldTileType.Fields, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.LowLand, new WorldTileModel(WorldTileType.LowLand, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Ruin, new WorldTileModel(WorldTileType.Ruin, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Village, new WorldTileModel(WorldTileType.Village, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Castle, new WorldTileModel(WorldTileType.Castle, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.AncientSite, new WorldTileModel(WorldTileType.AncientSite, 1, true, 0, 0, 0, 0));
        tileTypes.Add(WorldTileType.Undefined, new WorldTileModel(WorldTileType.Undefined, 1, true, 0, 0, 0, 0));

    }


    public static WorldTileModel Find(WorldTileType name)
    {
        if (tileTypes.ContainsKey(name))
        {
            return tileTypes[name];
        }
        return tileTypes[WorldTileType.Undefined];
    }

    public WorldTileType Name { get; }
    public int MovementCost { get;} 
    public bool Traversable { get; } 
    public int HealAttrition { get; } 
    public int RangeBonus { get;  } 
    public int AccuracyBonus { get;  } 
    public int AvoidanceBonus { get;} 
    public int DefenseBonus { get; } 

    protected WorldTileModel(WorldTileType name, int movementCost, bool traversable, int healAttrition, int rangeBonus, int accuracyBonus, int defenseBonus)
    {
        Name = name;
        MovementCost = movementCost;
        Traversable = traversable;
        HealAttrition = healAttrition;
        RangeBonus = rangeBonus;
        AccuracyBonus = accuracyBonus;
        DefenseBonus = defenseBonus;
    }
}