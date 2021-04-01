using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponTemplate;

[System.Serializable]
public class WeaponModel 
{
    private WeaponTemplate _template;
    private int _durability;

    public int Durability() => _durability;
    public bool IsBroken() => _durability == 0;
    public WeaponTemplate Template() => _template;


    public bool Durability(int value)
    {
        if(value >= 0 && value <= _template.DurabilityMax())
        {
            this._durability = value;
            return true;
        }
        return false;
    }

    private WeaponModel() { }

    public static WeaponModel create (WeaponTemplate type)
    {
        WeaponModel instance = new WeaponModel();
        instance._template = type;
        instance._durability = instance._template.DurabilityMax();
        return instance;
    }
}
