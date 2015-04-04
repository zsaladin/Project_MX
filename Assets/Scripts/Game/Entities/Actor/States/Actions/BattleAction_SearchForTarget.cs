using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    // Tatget Matching Algorithm will be interchangeable.
    public class BattleAction_SearchForTarget : BattleAction
    {
        public BattleActor Target { get; protected set; }

        public override ActionType Type
        {
            get
            {
                return ActionType.SearchForTarget;
            }
        }
        public BattleAction_SearchForTarget(BattleActor actor)
            : base(actor)
        {

        }
        public override void OnBegin()
        {
            base.OnBegin();

            SearchForTarget();
        }

        public override void OnTick()
        {
            SearchForTarget();
        }

        void SearchForTarget()
        {
            List<BattleActor> opponents = Manager.Entity.GetActors(Actor.OpponentOwnerShip, false);

            BattleActor nearestOpponent = null;
            float minDiff = float.MaxValue;

            Vector3 actorPosition = Actor.Position;
            int count = opponents.Count;
            for (int i = 0; i < count; ++i)
            {
                Vector3 diff = actorPosition - opponents[i].Position;
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
}