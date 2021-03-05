using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation
{
    public enum Key
    {
        Idle,
        Attack,
        Dodge,
        Push,
        Pushed,
        SM1,
        Die,
        LevelUp,
        Walk,
        Wound
    }

    private static readonly Dictionary<Key, UnitAnimation> animationMap = new Dictionary<Key, UnitAnimation>();

    public static UnitAnimation Find(Key name)
    {
        return animationMap.ContainsKey(name) ? animationMap[name] : animationMap[Key.Idle];
    }

    static UnitAnimation()
    {
        animationMap.Add(Key.Idle, new UnitAnimation(Key.Idle       , false, true));
        animationMap.Add(Key.Attack, new UnitAnimation(Key.Attack   , true, false));
        animationMap.Add(Key.Dodge, new UnitAnimation(Key.Dodge     , true, false));
        animationMap.Add(Key.Push, new UnitAnimation(Key.Push       , true, false));
        animationMap.Add(Key.Pushed, new UnitAnimation(Key.Pushed   , true, false));
        animationMap.Add(Key.SM1, new UnitAnimation(Key.SM1         , true, false));
        animationMap.Add(Key.Die, new UnitAnimation(Key.Die         , false, true));
        animationMap.Add(Key.LevelUp, new UnitAnimation(Key.LevelUp , true, false));
        animationMap.Add(Key.Walk, new UnitAnimation(Key.Walk       , false, false));
        animationMap.Add(Key.Wound, new UnitAnimation(Key.Wound     , true, false));
    }

    private readonly Key _keyName;
    private readonly bool _transitionAfterPlayedOnce;
    private readonly bool _restingAnimation;

    public Key KeyName => _keyName;
    public bool TransitionAfterPlayedOnce => _transitionAfterPlayedOnce;
    public bool RestingAnimation => _restingAnimation;

    private UnitAnimation(Key key, bool loop, bool rest)
    {
        this._keyName = key;
        this._transitionAfterPlayedOnce = loop;
        this._restingAnimation = rest;
    }

    public override bool Equals(object obj)
    {
        if(obj.GetType() == typeof(UnitAnimation))
        {
            UnitAnimation other = obj as UnitAnimation;
            return other._keyName.Equals(_keyName);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _keyName.GetHashCode();
    }
}
