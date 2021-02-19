using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTileType
{
    public static readonly string OCEAN = "ocean";
    public static readonly string SWAMP = "swamp";
    public static readonly string SHALLOWS = "shallows";
    public static readonly string DEEP_RIVER = "deep_river";
    public static readonly string SAND = "sand";
    public static readonly string DESERT = "desert";
    public static readonly string HILL = "hill";
    public static readonly string MOUNTAIN = "mountain";
    public static readonly string WOODS = "woods";
    public static readonly string DARK_FOREST = "dark_forest";
    public static readonly string FIELDS = "fields";
    public static readonly string LOWLAND = "lowland";
    public static readonly string RUIN = "ruin";
    public static readonly string VILLAGE = "village";
    public static readonly string CASTLE = "caste";
    public static readonly string ANCIENT_SITE = "ancient site";
    public static readonly string UNDEFINED = "undefined";

    private static readonly Dictionary<string, WorldTileType> tileTypes = new Dictionary<string, WorldTileType>();

    static WorldTileType()
    {
        tileTypes.Add(OCEAN, new WorldTileType(OCEAN, 1, false, 0, 0, 0, 0));
        tileTypes.Add(SWAMP, new WorldTileType(SWAMP, 1, true, 0, 0, 0, 0));
        tileTypes.Add(SHALLOWS, new WorldTileType(SHALLOWS, 1, true, 0, 0, 0, 0));
        tileTypes.Add(DEEP_RIVER, new WorldTileType(DEEP_RIVER, 1, true, 0, 0, 0, 0));
        tileTypes.Add(SAND, new WorldTileType(SAND, 1, true, 0, 0, 0, 0));
        tileTypes.Add(DESERT, new WorldTileType(DESERT, 1, true, 0, 0, 0, 0));
        tileTypes.Add(HILL, new WorldTileType(HILL, 1, true, 0, 0, 0, 0));
        tileTypes.Add(MOUNTAIN, new WorldTileType(MOUNTAIN, 1, false, 0, 0, 0, 0));
        tileTypes.Add(DARK_FOREST, new WorldTileType(DARK_FOREST, 1, false, 0, 0, 0, 0));
        tileTypes.Add(FIELDS, new WorldTileType(FIELDS, 1, true, 0, 0, 0, 0));
        tileTypes.Add(LOWLAND, new WorldTileType(LOWLAND, 1, true, 0, 0, 0, 0));
        tileTypes.Add(RUIN, new WorldTileType(RUIN, 1, true, 0, 0, 0, 0));
        tileTypes.Add(VILLAGE, new WorldTileType(VILLAGE, 1, true, 0, 0, 0, 0));
        tileTypes.Add(CASTLE, new WorldTileType(CASTLE, 1, true, 0, 0, 0, 0));
        tileTypes.Add(ANCIENT_SITE, new WorldTileType(ANCIENT_SITE, 1, true, 0, 0, 0, 0));
        tileTypes.Add(UNDEFINED, new WorldTileType(UNDEFINED, 1, true, 0, 0, 0, 0));

    }


    public static WorldTileType Find(string name)
    {
        if (tileTypes.ContainsKey(name.Trim()))
        {
            return tileTypes[name];
        }
        return tileTypes[UNDEFINED];
    }

    public string Name { get; }
    public int MovementCost { get;} 
    public bool Traversable { get; } 
    public int HealAttrition { get; } 
    public int RangeBonus { get;  } 
    public int AccuracyBonus { get;  } 
    public int AvoidanceBonus { get;} 
    public int DefenseBonus { get; } 

    protected WorldTileType(string name, int movementCost, bool traversable, int healAttrition, int rangeBonus, int accuracyBonus, int defenseBonus)
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