using static WeaponTemplate;
using System.Linq;
using System.Collections.Generic;
using static UnitTemplate;

[System.Serializable]
public class UnitModel
{
    private int _level;
    private UnitTemplate _template;
    private List<WeaponModel> _carriedWeapons;

    private int _hitPoints;
    private int _strength;
    private int _dexterity;
    private int _agility;
    private int _endurance;
    private int _bravery;

    public int Level() => _level;
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
    public int Resistance(UnitRank rank) => _template.BaseResistance(rank);

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
        
    public WeaponModel CurrentWeapon() => (_carriedWeapons.Count > 0) ? _carriedWeapons.ElementAt<WeaponModel>(0) : null;
    
}
