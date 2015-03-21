using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Death : BattleAction 
{
    private Animation _animation;
    public override void OnBegin()
    {
        base.OnBegin();
        Manager.Entity.RemoveActor(Actor);

        _animation = Actor.GetComponent<Animation>();
        if (_animation.GetClip("Die") != null)
        {
            _animation.wrapMode = WrapMode.Once;
            _animation.Play("Die");
        }
    }

    public override void Update()
    {
        base.Update();

        if (_animation.isPlaying == false)
        {
            GameObject.Destroy(Actor.gameObject);
        }
    }
}
