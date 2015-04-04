using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleActorBaseAction
    {
        private BattleActor _actor;

        private Vector3 _baseLauncherPoint;
        private Vector3 _baseProjectileHitPoint;

        public BattleActorBaseAction(BattleActor actor)
        {
            _actor = actor;

            _baseLauncherPoint = actor.transform.FindChildRecursively(Manager.Constant.ACTOR_LAUNCHER_POINT_NAME).localPosition;
            _baseLauncherPoint.x *= actor.transform.localScale.x;
            _baseLauncherPoint.y *= actor.transform.localScale.y;
            _baseLauncherPoint.z *= actor.transform.localScale.z;

            _baseProjectileHitPoint = actor.transform.FindChildRecursively(Manager.Constant.ACTOR_PROJECTILE_HIT_POINT_NAME).localPosition;
            _baseProjectileHitPoint.x *= actor.transform.localScale.x;
            _baseProjectileHitPoint.y *= actor.transform.localScale.y;
            _baseProjectileHitPoint.z *= actor.transform.localScale.z;
        }

        public void LookAt(Vector3 subjectPosition)
        {
            Vector3 direction = subjectPosition - _actor.Position;
            direction.y = 0;

            float angle = Mathf.Atan(direction.z / direction.x);
            angle -= 90 * Mathf.Deg2Rad;

            if (direction.magnitude / direction.x < 0)
                angle += 180 * Mathf.Deg2Rad;


            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);

            float tx = _baseLauncherPoint.x;
            float tz = _baseLauncherPoint.z;
            float x = (cos * tx) + (sin * tz);
            float z = (cos * tz) - (sin * tx);
            _actor.LauncherPoint = new Vector3(x, _baseLauncherPoint.y, z) + _actor.Position;


            tx = _baseProjectileHitPoint.x;
            tz = _baseProjectileHitPoint.z;
            x = (cos * tx) + (sin * tz);
            z = (cos * tz) - (sin * tx);
            _actor.ProjectileHitPoint = new Vector3(x, _baseProjectileHitPoint.y, z) + _actor.Position;


        }

        public void Update()
        {
        }
    }
}