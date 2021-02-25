using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponTemplate;

[System.Serializable]
public class UnitTemplate
{

    public enum UnitType {
        SolarKnight
    }
    
    public enum UnitRank
    {
        Recruit = 0,
        Veteran = 1
    }

    private static readonly Dictionary<UnitType, UnitTemplate> unitTemplates = new Dictionary<UnitType, UnitTemplate>();

    public static UnitTemplate Find(UnitType name) => (unitTemplates.ContainsKey(name)) ? unitTemplates[name] : unitTemplates[UnitType.SolarKnight];

    private UnitType _type;
    private WeaponClass _weaponMastered;

    private int _baseHitPoints;
    private int _baseStrenght;
    private int _baseDexterity;
    private int _baseAgility;
    private int _baseEndurance;
    private int _baseBravery;

    private float _baseGrowthHitPoints;
    private float _baseGrowthStrength;
    private float _baseGrowthDexterity;
    private float _baseGrowthAgility;
    private float _baseGrowthEndurance;
    private float _baseGrowthBravery;

    private int[] _mobility;
    private int[] _armorBlunt;
    private int[] _armorPiercing;
    private int[] _armorSlash;
    private int[] _resistance;

    public UnitType Type() => _type;
    public WeaponClass MasteredWeapon() => _weaponMastered;

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
    
    static UnitTemplate()
    {
        UnitTemplate solarKnight = new UnitTemplate();
        solarKnight._type = UnitType.SolarKnight;
        solarKnight._weaponMastered = WeaponClass.Sword;
        solarKnight._baseHitPoints = 10;
        solarKnight._baseStrenght = 3;
        solarKnight._baseDexterity = 3;
        solarKnight._baseEndurance = 3;
        solarKnight._baseAgility = 3;
        solarKnight._baseBravery = 5;
        solarKnight._baseGrowthHitPoints = 0.5f;
        solarKnight._baseGrowthStrength = 0.4f;
        solarKnight._baseGrowthDexterity = 0.2f;
        solarKnight._baseGrowthAgility = 0.2f;
        solarKnight._baseGrowthEndurance = 0.2f;
        solarKnight._baseGrowthBravery = 0.2f;
        solarKnight._mobility = new int[2]{ 4,5};
        solarKnight._armorBlunt = new int[2] { 1,2};
        solarKnight._armorPiercing = new int[2] { 0,1};
        solarKnight._armorSlash = new int[2] { 2, 3};
        solarKnight._resistance = new int[2] { 0,1}; 
        unitTemplates.Add(UnitType.SolarKnight, solarKnight);
    }

}
