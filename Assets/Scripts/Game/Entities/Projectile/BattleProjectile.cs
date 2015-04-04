using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleProjectile : MonoBehaviour, ITickable
    {
        private ProjectileProfile _profile;

        private BattleProjectileMotion _motion;
        private ProjectileTarget _target;

        public IProjectileReached ReachedHandler { set; get; }

        public Vector3 Position { get; set; }
        public Vector3 BeginPosition { get; private set; }
        public Vector3 TargetPosition
        {
            get
            {
                return _target.TargetPosition;
            }
        }

        public void Init(ProjectileProfile profile, Vector3 position)
        {
            _profile = profile;

            BeginPosition = Position = position;
            transform.position = position;

            _motion = BattleProjectileMotion.Create(_profile);
            _motion.Init(this);
        }

        public void SetTarget(Vector3 position)
        {
            _target = new ProjectileTarget(position);
        }

        public void SetTarget(BattleActor targetActor)
        {
            _target = new ProjectileTarget(targetActor);
        }

        public void OnTick()
        {
            if (ProejectileReached() == false)
                _motion.OnTick();
        }

        bool ProejectileReached()
        {
            if (_motion.IsEnded())
            {
                ReachedHandler.OnProjectileReached(Position);
                Manager.Entity.RemoveProjectile(this);
                Destroy(gameObject);
                return true;
            }

            return false;
        }

        void Update()
        {
            _motion.Update();
        }

        class ProjectileTarget
        {
            private Vector3 _targetPosition;
            private BattleActor _targetActor;

            public Vector3 TargetPosition
            {
                get
                {
                    if (_targetActor == null) return _targetPosition;
                    _targetPosition = _targetActor.ProjectileHitPoint;
                    return _targetPosition;
                }
            }

            public ProjectileTarget(Vector3 targetPosition)
            {
                _targetPosition = targetPosition;
            }

            public ProjectileTarget(BattleActor targetActor)
            {
                _targetActor = targetActor;
            }
        }
    }
}