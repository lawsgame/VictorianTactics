using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponTemplate;

public class WeaponModel 
{
    public WeaponTemplate Template { get; set; }

    public int _durability;

    public int Durability() => _durability;
    public bool IsBroken() => _durability == 0;

    public bool Durability(int value)
    {
        if(value >= 0 && value <= Template.DurabilityMax)
        {
            this._durability = value;
            return true;
        }
        return false;
    }

    private WeaponModel() { }

    public static WeaponModel create (WeaponType type)
    {
        WeaponModel instance = new WeaponModel();
        instance.Template = WeaponTemplate.Find(type);
        instance.Durability(instance.Template.DurabilityMax);
        return instance;
    }
}
