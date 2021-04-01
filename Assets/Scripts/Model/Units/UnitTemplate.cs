using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

[CreateAssetMenu(fileName ="New Unit Template", menuName = "Unit")]
public class UnitTemplate : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private WeaponClass[] _weaponMastered;

    [SerializeField] private int _baseHitPoints;
    [SerializeField] private int _baseStrenght;
    [SerializeField] private int _baseDexterity;
    [SerializeField] private int _baseAgility;
    [SerializeField] private int _baseEndurance;
    [SerializeField] private int _baseBravery;

    [SerializeField] private float _baseGrowthHitPoints;
    [SerializeField] private float _baseGrowthStrength;
    [SerializeField] private float _baseGrowthDexterity;
    [SerializeField] private float _baseGrowthAgility;
    [SerializeField] private float _baseGrowthEndurance;
    [SerializeField] private float _baseGrowthBravery;

    [SerializeField] private int[] _mobility;
    [SerializeField] private int[] _armorBlunt;
    [SerializeField] private int[] _armorPiercing;
    [SerializeField] private int[] _armorSlash;
    [SerializeField] private int[] _resistance;

    public string Name() => _name;
    public WeaponClass[] MasteredWeapon() => _weaponMastered;

    public int BaseHitPoints() => _baseHitPoints;
    public int BaseStrength() => _baseStrenght;
    public int BaseDexterity() => _baseDexterity;
    public int BaseAgility() => _baseAgility;
    public int BaseEndurance() => _baseEndurance;
    public int BaseBravery() => _baseBravery;

    public float BaseGrowthHitPoints() => _baseGrowthHitPoints;
    public float BaseGrowthStrength() => _baseGrowthStrength;
    public float BaseGrowthDexterity() => _baseGrowthDexterity;
    public float BaseGrowthAgility() => _baseGrowthAgility;
    public float BaseGrowthEndurance() => _baseGrowthEndurance;
    public float BaseGrowthBravery() => _baseGrowthBravery;

    public int BaseMobility(UnitRank rank) => _mobility[(int)rank];
    public int BaseArmorBlunt(UnitRank rank) => _armorBlunt[(int)rank];
    public int BaseArmorPiercing(UnitRank rank) => _armorPiercing[(int)rank];
    public int BaseArmorSlash(UnitRank rank) => _armorSlash[(int)rank];
    public int BaseResistance(UnitRank rank) => _resistance[(int)rank];

    public override bool Equals(object obj)
    {
        return obj is UnitTemplate template && template._name.Equals(_name);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
