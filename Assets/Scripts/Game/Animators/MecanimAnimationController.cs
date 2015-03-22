﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MecanimAnimationController : AnimationController 
{
    private Animator _animator;

    public override void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Play()
    {
        _animator.enabled = true;
    }

    public override void Play(AnimationType type)
    {
        _animator.enabled = true;
        _animator.Play(type.ToString());
    }

    public override void Stop()
    {
        _animator.enabled = false;
    }

    public override void Stop(AnimationType type)
    {
        _animator.enabled = false;
    }

    public override bool IsPlaying()
    {
        return _animator.enabled;
    }

    public override bool IsPlaying(AnimationType type)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(type.ToString());
    }
}