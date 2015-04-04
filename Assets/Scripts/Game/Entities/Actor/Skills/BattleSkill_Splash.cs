﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkill_Splash : BattleSkill, IProjectileReached
    {
        private float _radius;
        private float _damage;

        public BattleSkill_Splash(BattleSkillProfile profile, BattleActor actor)
            : base(profile, actor)
        {
            _radius = profile.Params.GetFloat("Radius").Value;
            _damage = profile.Params.GetFloat("Damage").Value;
        }

        public override void OnSkill()
        {
            base.OnSkill();
            Offense();

        }

        void Offense()
        {
            if (Actor.Target == null) return;

            if (Actor.OffenseType == OffenseType.Melee)
            {
                DealDamage();
            }
            else if (Actor.OffenseType == OffenseType.Range || Actor.OffenseType == OffenseType.Magic)
            {
                ProjectileProfile projectileProfile = Manager.Data.ProjectileProfileSave.Get(Actor.OffenseProjectileType);
                BattleProjectile projectile = Manager.Entity.CreateProjectile(projectileProfile, Actor.LauncherPoint);
                projectile.SetTarget(Actor.Target);
                projectile.ReachedHandler = this;
            }
        }

        void DealDamage()
        {
            Vector3 targetPos = Actor.Target.Position;
            targetPos.y = 0;

            var opponentActors = Manager.Entity.GetActors(Actor.OpponentOwnerShip, false);
            for (int i = 0; i < opponentActors.Count; ++i)
            {
                BattleActor opponentActor = opponentActors[i];
                BattleAction_Defense defenseAction = opponentActor.CurrentState.FindAction<BattleAction_Defense>();
                if (defenseAction == null) continue;

                Vector3 oppoPos = opponentActor.Position;
                oppoPos.y = 0;

                if ((oppoPos - targetPos).sqrMagnitude > _radius * _radius) continue;

                defenseAction.Defense(_damage);
            }
        }

        public void OnProjectileReached(Vector3 targetPosition)
        {
            DealDamage();
        }
    }
}