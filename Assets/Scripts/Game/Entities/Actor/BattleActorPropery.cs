using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleActorPropery
    {
        private BattleActor _actor;

        #region Raw Stat
        public float RawSize { get; private set; }
        public float RawHitPointMax { get; private set; }
        public float RawHitPoint { get; private set; }

        public OffenseType RawOffenseType { get; private set; }
        public int RawOffenseProjectileType { get; private set; }
        public float RawOffensePower { get; private set; }
        public float RawOffenseTime { get; private set; }
        public float RawOffenseDealTime { get; private set; }
        public float RawOffenseRange { get; private set; }

        public DefenseType RawDefenseType { get; private set; }

        public float RawMovingSpeed { get; private set; }
        #endregion

        #region Calculated Stat
        public float Size 
        { 
            get
            {
                return RawSize;
            }
        }

        public float HitPointMax 
        { 
            get
            {
                return RawHitPointMax;
            }
        }

        public float HitPoint 
        { 
            get
            {
                return RawHitPoint;
            }
            set
            {
                RawHitPoint = value;
            }

        }

        public OffenseType OffenseType
        {
            get
            {
                return RawOffenseType;
            }
        }

        public int OffenseProjectileType
        {
            get
            {
                return RawOffenseProjectileType;
            }
        }

        public float OffensePower
        {
            get
            {
                return RawOffensePower;
            }
        }

        public float OffenseTime
        {
            get
            {
                return RawOffenseTime;
            }
        }

        public float OffenseDealTime
        {
            get
            {
                return RawOffenseDealTime;
            }
        }

        public float OffenseRange
        {
            get
            {
                return RawOffenseRange;
            }
        }

        public DefenseType DefenseType
        {
            get
            {
                return RawDefenseType;
            }
        }

        public float MovingSpeed
        {
            get
            {
                return _actor.BuffMachine.GetBuffedMovementSpeed(); 
            }
        }

        #endregion

        public BattleActorPropery(BattleActor actor)
        {
            _actor = actor;

            RawSize = actor.Profile.Size;
            RawHitPoint = RawHitPointMax = actor.Profile.HitPointMax;
            RawOffenseType = actor.Profile.OffenseType;
            RawOffenseProjectileType = actor.Profile.OffenseProjectileType;
            RawOffensePower = actor.Profile.OffensePower;
            RawOffenseTime = actor.Profile.OffenseTime;
            RawOffenseDealTime = actor.Profile.OffenseDealTime;
            RawOffenseRange = actor.Profile.OffenseRange;

            RawDefenseType = actor.Profile.DefenseType;

            RawMovingSpeed = actor.Profile.MovingSpeed;
        }
    }
}