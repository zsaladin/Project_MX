using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Offense : BattleAction, IProjectileReached
    {
        protected BattleActor _target;

        protected float _currentDuratiion;
        protected bool _isDealed;

        public override ActionType Type
        {
            get
            {
                return ActionType.Offense;
            }
        }

        public BattleAction_Offense(BattleActor actor)
            : base(actor)
        {

        }

        public override void OnBegin()
        {
            base.OnBegin();

            _currentDuratiion = 0;
            _isDealed = false;

            Actor.AnimationController.Play(AnimationType.Attack);

            _target = Actor.Target;
        }

        public override void OnTick()
        {
            _currentDuratiion += Manager.Constant.GAME_TICK;

            if (_target != null)
            {
                Actor.BaseAction.LookAt(_target.Position);
            }

            if (_currentDuratiion > Actor.OffenseTime)
            {
                IsEnd = true;
                return;
            }

            if (_currentDuratiion >= Actor.OffenseDealTime)
            {
                if (_isDealed == false)
                {
                    _isDealed = true;
                    Offense();
                }
            }
        }

        public override void Update()
        {
            if (_target != null)
                Actor.transform.LookAt(_target.transform);
        }

        void Offense()
        {
            if (_target == null) return;

            if (Actor.OffenseType == OffenseType.Melee)
            {
                DealDamage();
            }
            else if (Actor.OffenseType == OffenseType.Range || Actor.OffenseType == OffenseType.Magic)
            {
                ProjectileProfile projectileProfile = Manager.Data.ProjectileProfileSave.Get(Actor.OffenseProjectileType);
                BattleProjectile projectile = Manager.Entity.CreateProjectile(projectileProfile, Actor.LauncherPoint);
                projectile.SetTarget(_target);
                projectile.ReachedHandler = this;
            }
        }

        void DealDamage()
        {
            var defenseAction = _target.StateMachine.CurrentState.FindAction<BattleAction_Defense>();
            if (defenseAction != null)
            {
                defenseAction.Defense(Actor.OffensePower);
            }
        }

        public void OnProjectileReached(Vector3 targetPosition)
        {
            DealDamage();
        }
    }


    public enum OffenseType
    {
        Invalid = 0x00,
        Melee = 0x01,
        Range = 0x02,
        Magic = 0x04,
    }
}