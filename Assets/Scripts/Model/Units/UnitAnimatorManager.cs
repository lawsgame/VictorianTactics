using UnityEngine;
using static Data;

[System.Serializable]
public class UnitAnimatorManager : MonoBehaviour
{
    private Unit _unit;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private UnitAnimation _currentAnimation;
    private Orientation _currentOrientation;

    private bool initiated = false;
    private UnitAnimation.Key _nextKey;
    private Orientation _nextOrientation;

    public Animator Animator => _animator;


    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _currentAnimation = UnitAnimation.Find(UnitAnimation.Key.Idle);
        _currentOrientation = _unit.InitialOrientation;
        _nextKey = UnitAnimation.Key.Idle;
        _nextOrientation = _unit.InitialOrientation;
    }

    void LateUpdate()
    {
        if (!initiated)
        {
            initiated = true;
            Launch();
        }
        else if (HasChanged())
        {
            Launch();
        }
        else if (!IsResting())
        {
            if (IsCurrentAnimationFinished())
            {
                Play(UnitAnimation.Key.Idle, _currentOrientation);
                Launch();
            }
        }
        
    }


    public void Play(UnitAnimation.Key key, Data.Orientation orientation)
    {
        _nextKey = key;
        _nextOrientation = orientation;
    }


    private void Launch()
    {
        _currentAnimation = UnitAnimation.Find(_nextKey);
        _currentOrientation = _nextOrientation;

        _renderer.flipX = _currentOrientation.Equals(Orientation.West) || _currentOrientation.Equals(Orientation.North);

        Orientation spriteOrientation = _currentOrientation;
        if (_currentOrientation.Equals(Orientation.West))
            spriteOrientation = Orientation.South;
        if (_currentOrientation.Equals(Orientation.North))
            spriteOrientation = Orientation.East;

        _animator.Play(string.Format("{0}{1}", _nextKey.ToString(), spriteOrientation.ToString()));
    }


    public bool IsResting() => _currentAnimation.RestingAnimation;


    private bool HasChanged() => _currentAnimation == null || !_currentAnimation.KeyName.Equals(_nextKey) || !_currentOrientation.Equals(_nextOrientation);


    private bool IsCurrentAnimationFinished()
    {
        return _currentAnimation.TransitionAfterPlayedOnce && _animator.GetCurrentAnimatorStateInfo(0).length < _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
