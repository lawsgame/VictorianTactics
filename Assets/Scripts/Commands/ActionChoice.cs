using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionChoice
{
    public enum Key
    {
        Attack,
        Move,
        Push,
        Heal,
        Visit,
        Plunnder,
        SwitchWeapon,
        SwitchPosition,
        ChooseOrientation,
        nothing
    }

    public enum Doable{
        Indefinitly,
        act,
        move
    }

    private Dictionary<Key, ActionChoice> choices = new Dictionary<Key, ActionChoice>();

    static ActionChoice()
    {

    }

}
