using System.Collections;
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
        tileTemplate.Add(WorldTileType.Woods, new WorldTileTemplate(WorldTileType.Woods, 2, true, 0, 0, 0, 0));
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

    private WorldTileType _name;
    private int _movementCost;
    private bool _traversable;
    private int _healAttrition;
    private int _rangeBonus;
    private int _accuracyBonus;
    private int _avoidanceBonus;
    private int _defenseBonus;

    public WorldTileType Name => _name;
    public int MovementCost() => _movementCost;
    public bool Traversable => _traversable;
    public int HealAttrition => _healAttrition;
    public int RangeBonus => _rangeBonus;
    public int AccuracyBonus => _accuracyBonus;
    public int AvoidanceBonus => _avoidanceBonus;
    public int DefenseBonus => _defenseBonus;
    
    public int MovementCost(UnitModel unit) => MovementCost();  

    protected WorldTileTemplate(WorldTileType name, int movementCost, bool traversable, int healAttrition, int rangeBonus, int accuracyBonus, int defenseBonus)
    {
        this._name = name;
        this._movementCost = movementCost;
        this._traversable = traversable;
        this._healAttrition = healAttrition;
        this._rangeBonus = rangeBonus;
        this._accuracyBonus = accuracyBonus;
        this._defenseBonus = defenseBonus;
    }
}