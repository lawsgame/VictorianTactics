using static WeaponTemplate;
using System.Linq;
using System.Collections.Generic;
using static UnitTemplate;
using UnityEngine;
using static Data;

[System.Serializable]
public class UnitModel
{
    // battle attributes
    public bool acted = false;
    public bool moved = false;
    public Orientation orientation;

    // general attributes
    private int _level;
    private int _party;
    private UnitTemplate _template;
    private List<WeaponModel> _carriedWeapons;
    private int _hitPoints;
    private int _strength;
    private int _dexterity;
    private int _agility;
    private int _endurance;
    private int _bravery;

    public int Level() => _level;
    public int Party() => _party;
    public UnitTemplate Template() => _template;
    public UnitRank Rank() => (_level < Data.LEVEL_PROMOTION) ? UnitRank.Recruit: UnitRank.Veteran;
    public WeaponClass MasteredWeapon() => _template.MasteredWeapon();
    public List<WeaponModel> CarriedWeapon() => _carriedWeapons;

    public int HitPoints() => _hitPoints;
    public int Strength() => _strength;
    public int Dexterity() => _dexterity;
    public int Agility() => _agility;
    public int Endurance() => _endurance;
    public int Bravery() => _bravery;
    public int Mobility(UnitRank rank) => _template.BaseMobility(rank);
    public int Mobility() => Mobility(Rank());
    public int Resistance(UnitRank rank) => _template.BaseResistance(rank);
    public int Resistance() => Resistance(Rank());

    public int Armor(DamageType type, UnitRank rank)
    {
        switch (type)
        {
            case DamageType.BLUNT: return _template.BaseArmorBlunt(rank);
            case DamageType.SLASH: return _template.BaseArmorSlash(rank);
            case DamageType.PIERCING: return _template.BaseArmorPiercing(rank);
            case DamageType.POISON: return _template.BaseResistance(rank);
            case DamageType.FIRE: return _template.BaseResistance(rank);
        }
        return 0;
    }
        
    public WeaponModel CurrentWeapon() => (_carriedWeapons != null &&_carriedWeapons.Count > 0) ? _carriedWeapons.ElementAt<WeaponModel>(0) : null;

    private UnitModel() { }

    public static UnitModel create(int startLevel, int party, UnitType type, List<WeaponModel> carriedWeapons, bool randomLevelUp, Orientation initialOrientation)
    {
        UnitModel unit = new UnitModel();
        unit.orientation = initialOrientation;
        unit._level = 1;
        unit._party = party;
        unit._template = UnitTemplate.Find(type);
        unit._carriedWeapons = carriedWeapons;
        unit._agility = unit._template.BaseAgility();
        unit._bravery = unit._template.BaseBravery();
        unit._dexterity = unit._template.BaseDexterity();
        unit._endurance = unit._template.BaseEndurance();
        unit._hitPoints = unit._template.BaseHitPoints();
        unit._strength = unit._template.BaseStrength();

        if (randomLevelUp)
        {
            while (unit._level < startLevel)
            {
                unit.levelUp();
            }
        }
        else
        {
            unit._agility += (int)((startLevel - 1) * unit._template.BaseGrowthAgility()) ;
            unit._bravery += (int)((startLevel - 1) * unit._template.BaseGrowthBravery());
            unit._dexterity += (int)((startLevel - 1) * unit._template.BaseGrowthDexterity());
            unit._endurance += (int)((startLevel - 1) * unit._template.BaseGrowthEndurance());
            unit._hitPoints += (int)((startLevel - 1) * unit._template.BaseGrowthHitPoints());
            unit._strength += (int)((startLevel - 1) * unit._template.BaseGrowthStrength());
        }
        

        return unit;
    }

    public int[] levelUp()
    {
        _level++;
        int[] statGained = new int[6];
        statGained[0] = (Random.value < _template.BaseGrowthHitPoints()) ? 1 : 0;
        statGained[1] = (Random.value < _template.BaseGrowthStrength()) ? 1 : 0;
        statGained[2] = (Random.value < _template.BaseGrowthDexterity()) ? 1 : 0;
        statGained[3] = (Random.value < _template.BaseGrowthEndurance()) ? 1 : 0;
        statGained[4] = (Random.value < _template.BaseGrowthAgility()) ? 1 : 0;
        statGained[5] = (Random.value < _template.BaseGrowthBravery()) ? 1 : 0;

        _hitPoints += statGained[0];
        _strength += statGained[1];
        _dexterity += statGained[2];
        _endurance += statGained[3];
        _agility += statGained[4];
        _bravery += statGained[5];

        return statGained;
    }

    public override string ToString()
    {
        return string.Format("Unit({0} lvl {1})", _template.Type(),_level); 
    }

    public bool SameSide(UnitModel model) => model._party == this._party;
    
}
