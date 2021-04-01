using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{

    public GameObject decoration;

    [SerializeField] private string _name;
    [SerializeField] private int _movementCost;
    [SerializeField] private bool _traversable;
    [SerializeField] private int _healAttrition;
    [SerializeField] private int _rangeBonus;
    [SerializeField] private int _accuracyBonus;
    [SerializeField] private int _avoidanceBonus;
    [SerializeField] private int _defenseBonus;

    public string Name => _name;
    public int MovementCost() => _movementCost;
    public bool Traversable => _traversable;
    public int HealAttrition => _healAttrition;
    public int RangeBonus => _rangeBonus;
    public int AccuracyBonus => _accuracyBonus;
    public int AvoidanceBonus => _avoidanceBonus;
    public int DefenseBonus => _defenseBonus;

    public int MovementCost(UnitModel unit) => MovementCost();

    public bool HasDecoration { get => decoration != null;  }


    public override string ToString()
    {
        return string.Format("Tile({0}) ", _name);
    }
}
