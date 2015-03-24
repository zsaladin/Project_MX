using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleProjectileMotion_Arc : BattleProjectileMotion 
{
    const float _gravityAcc = -20f;
    bool _isEnded = false;
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

        float totalSecond = totalDistance / _speed;
        float nextSecond = _isEnded ? totalSecond : (currentDistance + nextDeltaDistance) / _speed;

        float diffHeight = _projectitle.TargetPosition.y - _projectitle.BeginPosition.y;
        float beginVelocity = (diffHeight - (0.5f * _gravityAcc * totalSecond * totalSecond)) / totalSecond;

        float nextHeight = nextSecond * beginVelocity + (0.5f * _gravityAcc * nextSecond * nextSecond);

        nextPosition.y = _projectitle.BeginPosition.y + nextHeight;
        _projectitle.Position = nextPosition;
    }

    public override bool IsEnded()
    {
        return _isEnded;
    }
}
