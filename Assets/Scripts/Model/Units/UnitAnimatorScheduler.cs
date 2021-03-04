using UnityEngine;
using static Data;

[System.Serializable]
public class UnitAnimatorScheduler : MonoBehaviour
{

    private Animator _animator;
    private SpriteRenderer _renderer;
    private AnimationKey _currentKey;
    private Orientation _currentOrientation;

    public Animator Animator => _animator;

    public enum AnimationKey
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


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Play(AnimationKey key, Data.Orientation orientation)
    {
        if (_currentOrientation.Equals(orientation) || key.Equals(_currentKey))
            return;

        _currentKey = key;
        _currentOrientation = orientation;
        _renderer.flipX = orientation.Equals(Orientation.West) || orientation.Equals(Orientation.North);

        Orientation spriteOrientation = orientation;
        if (orientation.Equals(Orientation.West))
            spriteOrientation = Orientation.South;
        if (orientation.Equals(Orientation.North))
            spriteOrientation = Orientation.East;

        _animator.Play(string.Format("{0}{1}", key.ToString(), orientation.ToString()));
    }

    

}
