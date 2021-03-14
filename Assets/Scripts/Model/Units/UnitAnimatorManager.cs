using System.Collections.Generic;
using UnityEngine;
using static Data;

[System.Serializable]
public class UnitAnimatorManager : MonoBehaviour
{
    private Unit _unit;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool initiated = false;
    private UnitAnimation _currentAnimation;
    private Queue<(UnitAnimation.Key key, Orientation orientation)>  _animQueue;

    public Animator Animator => _animator;


    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _currentAnimation = UnitAnimation.Find(UnitAnimation.Key.Idle);
        _animQueue = new Queue<(UnitAnimation.Key key, Orientation orientation)>();
    }

    void LateUpdate()
    {
        if (!initiated)
        {
            initiated = true;
            PlayNext(_currentAnimation.KeyName, _unit.Model.currentOrientation);
        }
        else if (IsResting())
        {
            if(HasNext())
                PlayNext();
        }
        else if (IsCurrentAnimationFinished())
        { 
            if (!HasNext())
            {
                QueueNext(UnitAnimation.Key.Idle, _unit.Model.currentOrientation);
            }
            PlayNext();
            
        }
        
    }


    public void QueueNext(UnitAnimation.Key key, Data.Orientation orientation)
    {
        _animQueue.Enqueue((key, orientation));
    }


    private void PlayNext()
    {
        if(_animQueue.Count > 0)
        {
            (UnitAnimation.Key key, Orientation or) next = _animQueue.Dequeue();
            PlayNext(next.key, next.or);
            
        }
        
    }

    private void PlayNext(UnitAnimation.Key nextKey, Data.Orientation nextOrientation)
    {
        UnitAnimation nextAnimation = UnitAnimation.Find(nextKey);
        _currentAnimation = nextAnimation;
        _unit.Model.currentOrientation = nextOrientation;
        _renderer.flipX = nextOrientation.Equals(Orientation.West) || nextOrientation.Equals(Orientation.North);

        Orientation spriteOrientation = nextOrientation;
        if (nextOrientation.Equals(Orientation.West))
            spriteOrientation = Orientation.South;
        if (nextOrientation.Equals(Orientation.North))
            spriteOrientation = Orientation.East;

        string animationStateName = string.Format("{0}{1}", nextKey.ToString(), spriteOrientation.ToString());
        Debug.Log("play next: " + animationStateName);
        _animator.Play(animationStateName);
    }


    public bool IsResting() => _currentAnimation.RestingAnimation;


    private bool HasNext() => _animQueue != null && _animQueue.Count > 0;


    private bool IsCurrentAnimationFinished()
    {
        return _currentAnimation.TransitionAfterPlayedOnce 
            && _animator.GetCurrentAnimatorStateInfo(0).length < _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
