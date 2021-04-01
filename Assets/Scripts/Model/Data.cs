using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data 
{
    public readonly static int LEVEL_MAX = 20;
    public readonly static int LEVEL_PROMOTION = 10;

    public enum Orientation 
    { 
        
        South, 
        West,
        North,
        East
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
}
