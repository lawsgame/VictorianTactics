﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTileTemplate
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

    private static readonly Dictionary<WorldTileType, WorldTileTemplate> tileTemplate = new Dictionary<WorldTileType, WorldTileTemplate>();

    static WorldTileTemplate()
    {
        tileTemplate.Add(WorldTileType.Ocean, new WorldTileTemplate(WorldTileType.Ocean, 1, false, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Swamp, new WorldTileTemplate(WorldTileType.Swamp, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Swallow, new WorldTileTemplate(WorldTileType.Swallow, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.DeepRiver, new WorldTileTemplate(WorldTileType.DeepRiver, 1, false, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Sand, new WorldTileTemplate(WorldTileType.Sand, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Desert, new WorldTileTemplate(WorldTileType.Desert, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Hill, new WorldTileTemplate(WorldTileType.Hill, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Mountain, new WorldTileTemplate(WorldTileType.Mountain, 1, false, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Woods, new WorldTileTemplate(WorldTileType.Woods, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.DarkForest, new WorldTileTemplate(WorldTileType.DarkForest, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Fields, new WorldTileTemplate(WorldTileType.Fields, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.LowLand, new WorldTileTemplate(WorldTileType.LowLand, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Ruin, new WorldTileTemplate(WorldTileType.Ruin, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Village, new WorldTileTemplate(WorldTileType.Village, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Castle, new WorldTileTemplate(WorldTileType.Castle, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.AncientSite, new WorldTileTemplate(WorldTileType.AncientSite, 1, true, 0, 0, 0, 0));
        tileTemplate.Add(WorldTileType.Undefined, new WorldTileTemplate(WorldTileType.Undefined, 1, true, 0, 0, 0, 0));

    }


    public static WorldTileTemplate Find(WorldTileType name)
    {
        if (tileTemplate.ContainsKey(name))
        {
            return tileTemplate[name];
        }
        return tileTemplate[WorldTileType.Undefined];
    }

    public WorldTileType Name { get; }
    public int MovementCost { get;} 
    public bool Traversable { get; } 
    public int HealAttrition { get; } 
    public int RangeBonus { get;  } 
    public int AccuracyBonus { get;  } 
    public int AvoidanceBonus { get;} 
    public int DefenseBonus { get; } 

    protected WorldTileTemplate(WorldTileType name, int movementCost, bool traversable, int healAttrition, int rangeBonus, int accuracyBonus, int defenseBonus)
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