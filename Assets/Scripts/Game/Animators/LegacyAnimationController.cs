using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LegacyAnimationController : AnimationController
{
    private Animation _animation;

    public override void Init()
    {
        _animation = GetComponent<Animation>();
    }

    public override void Play()
    {
        _animation.Play();    
    }

    public override void Play(AnimationType type)
    {
        _animation.Play(type.ToString());
    }

    public override void Stop()
    {
        _animation.Stop();
    }

    public override void Stop(AnimationType type)
    {
        _animation.Stop(type.ToString());
    }

    public override bool IsPlaying()
    {
        return _animation.isPlaying;   
    }

    public override bool IsPlaying(AnimationType type)
    {
        return _animation.IsPlaying(type.ToString());
    }
}
