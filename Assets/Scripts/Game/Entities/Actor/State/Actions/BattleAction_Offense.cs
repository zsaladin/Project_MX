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

            if (_currentDuratiion > Actor.Property.OffenseTime)
            {
                IsEnd = true;
                return;
            }

            if (_currentDuratiion >= Actor.Property.OffenseDealTime)
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
            {
                Vector3 targetPos = _target.transform.position;
                targetPos.y = Actor.transform.position.y;
                Actor.transform.LookAt(targetPos);
            }
        }

        void Offense()
        {
            if (_target == null) return;

            if (Actor.Property.OffenseType == OffenseType.Melee)
            {
                DealDamage();
            }
            else if (Actor.Property.OffenseType == OffenseType.Range || Actor.Property.OffenseType == OffenseType.Magic)
            {
                ProjectileProfile projectileProfile = Manager.Data.ProjectileProfileSave.Get(Actor.Property.OffenseProjectileType);
                BattleProjectile projectile = Manager.Entity.CreateProjectile(projectileProfile, Actor.LauncherPoint);
                projectile.SetTarget(_target);
                projectile.ReachedHandler = this;
            }

            OnDealEffects();
        }

        void DealDamage()
        {
            var defenseAction = _target.StateMachine.CurrentState.FindAction<BattleAction_Defense>();
            if (defenseAction != null)
            {
                defenseAction.Defense(Actor.Property.OffensePower);
            }
            OnHitEffects();
            OnSpotEffects();
        }

        public void OnProjectileReached(Vector3 targetPosition)
        {
            DealDamage();
        }

        protected void OnDealEffects()
        {
            for (int i = 0; i < Actor.Profile.OnDealEffects.Count; ++i)
            {
                var effectProfile = Actor.Profile.OnDealEffects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                effectObject.AddComponent<EffectController>().Init(effectProfile, Actor);
            }
        }

        protected void OnHitEffects()
        {
            for (int i = 0; i < Actor.Profile.OnHitEffects.Count; ++i)
            {
                var effectProfile = Actor.Profile.OnHitEffects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                if (_target != null)
                    effectObject.AddComponent<EffectController>().Init(effectProfile, _target);
            }
        }

        protected void OnSpotEffects()
        {
            for (int i = 0; i < Actor.Profile.OnSpotEffects.Count; ++i)
            {
                var effectProfile = Actor.Profile.OnSpotEffects[i];
                GameObject effectObject = GameObject.Instantiate<GameObject>(effectProfile.Prefab);
                effectObject.AddComponent<EffectController>().Init(effectProfile, _target.Position);
            }
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