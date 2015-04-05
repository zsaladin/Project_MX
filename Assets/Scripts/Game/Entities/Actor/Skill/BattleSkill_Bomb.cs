using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleSkill_Bomb : BattleSkill, IProjectileReached
    {
        private ProjectileProfile _projectileProfile;

        private int _projectileID;
        private float _radius;

        public BattleSkill_Bomb(BattleSkillProfile profile, BattleActor actor)
            : base(profile, actor)
        {
            _projectileID = profile.Params.GetInt("ProjectileID").Value;

            _radius = profile.Params.GetFloat("Radius").Value;
            _damage = profile.Params.GetFloat("Damage").Value;

            _projectileProfile = Manager.Data.ProjectileProfileSave.Get(_projectileID);
        }

        public override void OnSkill()
        {
            base.OnSkill();

            if (Actor.Target == null) return;

            var projectile = Manager.Entity.CreateProjectile(_projectileProfile, Actor.Position);
            projectile.SetTarget(Actor.Target.Position);
            projectile.ReachedHandler = this;
        }

        public override void OnTick()
        {
            base.OnTick();
        }

        public void OnProjectileReached(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;

            Vector3 targetPos = targetPosition;
            targetPos.y = 0;

            var opponentActors = Manager.Entity.GetActors(Actor.OpponentOwnerShip, false);
            for (int i = 0; i < opponentActors.Count; ++i)
            {
                BattleActor opponentActor = opponentActors[i];

                Vector3 oppoPos = opponentActor.Position;
                oppoPos.y = 0;
                if ((oppoPos - targetPos).sqrMagnitude > _radius * _radius) continue;

                _targetActors.Add(opponentActor);
            }

            Deal();
        }

    }
}