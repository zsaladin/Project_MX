using UnityEngine;
using System.Collections.Generic;
using System.Linq;


// Tatget Matching Algorithm will be interchangeable.
public class BattleAction_SearchForTarget : BattleAction 
{
    public BattleActor Target { get; protected set; }

    public override void OnTick()
    {
        //Debug.Log(string.Format("{0} : {1}", Actor.name, GetType().Name));
        List<BattleActor> opponents = Manager.Entity.GetActors(Actor.OpponentOwnerShip, false);

        BattleActor nearestOpponent = null;
        float minDiff = float.MaxValue;

        Vector3 actorPosition = Actor.transform.position;
        int count = opponents.Count;
        for(int i = 0; i < count; ++i)
        {
            Vector3 diff = actorPosition - opponents[i].transform.position;
            float sqrMag = diff.sqrMagnitude;
            if (minDiff > sqrMag)
            {
                minDiff = sqrMag;
                nearestOpponent = opponents[i];
            }
        }

        Target = nearestOpponent;
    }
}
