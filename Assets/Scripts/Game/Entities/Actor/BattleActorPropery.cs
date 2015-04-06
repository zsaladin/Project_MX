using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleActorPropery
    {
        private BattleActor _actor;

        public float Size { get; private set; }
        public float HitPointMax { get; private set; }
        public float HitPoint { get; set; }

        public OffenseType OffenseType { get; private set; }
        public int OffenseProjectileType { get; private set; }
        public float OffensePower { get; private set; }
        public float OffenseTime { get; private set; }
        public float OffenseDealTime { get; private set; }
        public float OffenseRange { get; private set; }

        public DefenseType DefenseType { get; private set; }

        public float MovingSpeed { get; private set; }

        public BattleActorPropery(BattleActor actor)
        {
            _actor = actor;

            Size = actor.Profile.Size;
            HitPoint = HitPointMax = actor.Profile.HitPointMax;
            OffenseType = actor.Profile.OffenseType;
            OffenseProjectileType = actor.Profile.OffenseProjectileType;
            OffensePower = actor.Profile.OffensePower;
            OffenseTime = actor.Profile.OffenseTime;
            OffenseDealTime = actor.Profile.OffenseDealTime;
            OffenseRange = actor.Profile.OffenseRange;

            DefenseType = actor.Profile.DefenseType;

            MovingSpeed = actor.Profile.MovingSpeed;
        }
    }
}