using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Wait : BattleAction 
{
    public override void OnBegin()
    {
        base.OnBegin();

        Actor.AnimationController.Play(AnimationType.Idle);
    }
}
