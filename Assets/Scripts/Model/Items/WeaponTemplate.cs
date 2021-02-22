using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    static WeaponTemplate()
    {
        WeaponTemplate shortSword = new WeaponTemplate();
        shortSword.Type = WeaponType.ShortSword;
        shortSword.Class = WeaponClass.Sword;
        shortSword.SubClass = WeaponSubClass.Standard;
        shortSword.RangeMin = 1;
        shortSword.RangeMax = 1;
        shortSword.PowerMin = 3;
        shortSword.PowerMax = 5;
        shortSword.DamageType = DamageType.SLASH;
        shortSword.Weight = 6;
        shortSword.Accuracy = 80;
        shortSword.DropRate = 0.2f;
        shortSword.DurabilityMax = 40;
        weaponTemplates.Add(WeaponType.ShortSword, shortSword);
    }


    public static WeaponTemplate Find(WeaponType name)
    {
        if (weaponTemplates.ContainsKey(name))
        {
            return weaponTemplates[name];
        }
        return weaponTemplates[WeaponType.Undefinied];
    }

    public WeaponType Type { get; private set; }
    public WeaponClass Class { get; private set; }
    public WeaponSubClass SubClass { get; private set; }
    public int RangeMin { get; private set; }
    public int RangeMax { get; private set; }
    public int PowerMin { get; private set; }
    public int PowerMax { get; private set; }
    public DamageType DamageType { get; private set; }
    public int Weight { get; private set; }
    public int Accuracy { get; private set; }
    public float DropRate { get; private set; }
    public int DurabilityMax { get; private set; }


    
}
