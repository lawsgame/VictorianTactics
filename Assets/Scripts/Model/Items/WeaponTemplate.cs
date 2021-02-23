using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTemplate
{
    public enum WeaponType{
        ShortSword,
        Undefinied
    }

    public enum WeaponClass
    {
        Sword,
        Spear,
        Mace,
        Axe,
        Bow,
        Firearm,
        TechArm
    }

    public enum WeaponSubClass
    {
        Standard
    }


    private static readonly Dictionary<WeaponType, WeaponTemplate> weaponTemplates = new Dictionary<WeaponType, WeaponTemplate>();

    public static WeaponTemplate Find(WeaponType name)
    {
        if (weaponTemplates.ContainsKey(name))
        {
            return weaponTemplates[name];
        }
        return weaponTemplates[WeaponType.ShortSword];
    }

    private WeaponType type;
    private WeaponClass clazz;
    private WeaponSubClass subClazz;
    private int rangeMin;
    private int rangeMax;
    private int powerMin;
    private int powerMax;
    private DamageType damageType;
    private int weight;
    private int accuracy;
    private float dropRate;
    private int durabilityMax;

    public WeaponType Type() => type;
    public WeaponClass Class() => clazz;
    public WeaponSubClass SubClass() => subClazz;
    public int RangeMin() => rangeMin;
    public int RangeMax() => rangeMax;
    public int PowerMin() => powerMin;
    public int PowerMax() => powerMax;
    public DamageType GetDamageType() => damageType;
    public int Weight() => weight;
    public int Accuracy() => accuracy;
    public float DropRate() => dropRate;
    public int DurabilityMax() => durabilityMax;

    static WeaponTemplate()
    {
        WeaponTemplate shortSword = new WeaponTemplate();
        shortSword.type = WeaponType.ShortSword;
        shortSword.clazz = WeaponClass.Sword;
        shortSword.subClazz = WeaponSubClass.Standard;
        shortSword.rangeMin = 1;
        shortSword.rangeMax = 1;
        shortSword.powerMin = 3;
        shortSword.powerMax = 5;
        shortSword.damageType = DamageType.SLASH;
        shortSword.weight = 6;
        shortSword.accuracy = 80;
        shortSword.dropRate = 0.2f;
        shortSword.durabilityMax = 40;
        weaponTemplates.Add(WeaponType.ShortSword, shortSword);
    }

}
