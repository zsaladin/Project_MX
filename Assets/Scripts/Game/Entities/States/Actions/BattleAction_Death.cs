using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Death : BattleAction 
{
    public override void OnBegin()
    {
        base.OnBegin();
        Manager.Entity.RemoveActor(Actor);

        Actor.AnimationController.Play(AnimationType.Die);
    }

    public override void Update()
    {
        base.Update();

        if (Actor.AnimationController.IsPlaying(AnimationType.Die) == false)
        {
            GameObject.Destroy(Actor.gameObject);
        }
    }
}
