using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class AnimationController : MonoBehaviour 
{
    public abstract void Init();
    public abstract void Play();
    public abstract void Play(AnimationType type);
    public abstract void Stop();
    public abstract void Stop(AnimationType type);
    public abstract bool IsPlaying();
    public abstract bool IsPlaying(AnimationType type);
}

public enum AnimationType
{
    Invalid,
    Idle,
    Walk,
    Run,
    Die,
    Attack,
    SkillAttack,
    Casting,
}
