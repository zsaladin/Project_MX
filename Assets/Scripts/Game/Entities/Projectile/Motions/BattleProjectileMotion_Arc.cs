using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleProjectileMotion_Arc : BattleProjectileMotion
    {
        const float _gravityAcc = -20f;
        bool _isEnded = false;

        float _nextSecond;
        float _totalSecond;
        float _beginVelocity;

        public override void OnTick()
        {
            Vector3 targetPosition = _projectitle.TargetPosition;
            targetPosition.y = 0;

            Vector3 position = _projectitle.Position;
            position.y = 0;

            Vector3 direction = (targetPosition - position).normalized;

            float nextDeltaDistance = _speed * Manager.Constant.GAME_TICK;
            Vector3 nextPosition = _projectitle.Position + direction * nextDeltaDistance;


            Vector3 totalVector = _projectitle.TargetPosition - _projectitle.BeginPosition;
            totalVector.y = 0;
            float totalDistance = totalVector.magnitude;

            Vector3 currentVector = _projectitle.Position - _projectitle.BeginPosition;
            currentVector.y = 0;
            float currentDistance = currentVector.magnitude;

            float nextRatio = (currentDistance + nextDeltaDistance) / totalDistance;
            if (nextRatio >= 1f)
            {
                _isEnded = true;
            }

            _totalSecond = totalDistance / _speed;
            _nextSecond = _isEnded ? _totalSecond : (currentDistance + nextDeltaDistance) / _speed;

            float diffHeight = _projectitle.TargetPosition.y - _projectitle.BeginPosition.y;
            _beginVelocity = (diffHeight - (0.5f * _gravityAcc * _totalSecond * _totalSecond)) / _totalSecond;

            float nextHeight = _nextSecond * _beginVelocity + (0.5f * _gravityAcc * _nextSecond * _nextSecond);

            nextPosition.y = _projectitle.BeginPosition.y + nextHeight;
            _projectitle.Position = nextPosition;
        }

        public override void Update()
        {
            Vector3 projTransXZPosition = _projectitle.transform.position;
            Vector3 projXZPosition = _projectitle.Position;
            projTransXZPosition.y = 0;
            projXZPosition.y = 0;
            Vector3 nextPos = Vector3.MoveTowards(projTransXZPosition, projXZPosition, _speed * Time.deltaTime);

            float currentAcc = _beginVelocity + _gravityAcc * (_nextSecond - Manager.Constant.GAME_TICK);

            float transY = _projectitle.transform.position.y;
            nextPos.y = transY + currentAcc * Time.deltaTime;
            _projectitle.transform.position = nextPos;
        }

        public override bool IsEnded()
        {
            return _isEnded;
        }
    }
}