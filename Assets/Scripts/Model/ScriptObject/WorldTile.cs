using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class WorldTile : Tile
{

    [SerializeField] private string tileName;
    [SerializeField] private int movementCost;
    [SerializeField] private bool traversable;
    [SerializeField] private int healAttrition;
    [SerializeField] private int rangeBonus;
    [SerializeField] private int accuracyBonus;
    [SerializeField] private int avoidanceBonus;
    [SerializeField] private int defenseBonus;

    public string TileName { get => tileName;  }
    public int MovementCost { get => movementCost;  }
    public bool Traversable { get => traversable;  }
    public int HealAttrition { get => healAttrition;  }
    public int RangeBonus { get => rangeBonus;  }
    public int AccuracyBonus { get => accuracyBonus;  }
    public int AvoidanceBonus { get => avoidanceBonus;  }
    public int DefenseBonus { get => defenseBonus;  }

    public override string ToString()
    {
        return string.Format("Tile({0}) ", TileName);
    }
}
