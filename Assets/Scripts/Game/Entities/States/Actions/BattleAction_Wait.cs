using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Wait : BattleAction 
{
    public override void OnBegin()
    {
        base.OnBegin();

        Animation animaition = Actor.GetComponent<Animation>();
        if (animaition.GetClip("idle"))
        {
            animaition.Play("idle");
        }
        else
        {
            animaition.Play(Random.Range(0f, 1f) < 0.5f ? "Idle_01" : "Idle_02");
        }
    }
}
