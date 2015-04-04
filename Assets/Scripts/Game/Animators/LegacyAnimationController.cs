using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class LegacyAnimationController : AnimationController
    {
        private Animation _animation;

        public override void Init()
        {
            _animation = GetComponent<Animation>();
        }

        public override void Play(float speed = 1f)
        {
            _animation.Play();
            _animation[_animation.clip.name].speed = speed;
        }

        public override void Play(AnimationType type, float speed = 1f)
        {
            _animation.Play(type.ToString());
            _animation[type.ToString()].speed = speed;
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
}