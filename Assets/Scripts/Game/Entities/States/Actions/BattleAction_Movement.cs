using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Movement : BattleAction
{
    private List<Vector3> _totalPathPositions;
    private List<Vector3> _effectivePathPositions = new List<Vector3>();

    private int _finalPathIndex;

    public override ActionType Type
    {
        get
        {
            return ActionType.Movement;
        }
    }

    public override void OnBegin()
    {
        base.OnBegin();

        Actor.AnimationController.Play(AnimationType.Walk);
    }

    public override void OnTick()
    {
        _totalPathPositions = Manager.Coordinate.GetPath(Actor);
        _effectivePathPositions.Clear();
        _finalPathIndex = 0;

        if (_totalPathPositions == null) return;
        if (_totalPathPositions.Count == 0) return;

        float totalMoveDistance = Actor.MovingSpeed * Manager.Constant.GAME_TICK;

        for (int i = 1; i <= _totalPathPositions.Count; ++i)
        {
            Vector3 destination = _totalPathPositions[_totalPathPositions.Count - i];
            _effectivePathPositions.Add(destination);

            Vector3 nextPosition = Vector3.MoveTowards(Actor.Position, destination, totalMoveDistance);
            float moveDelta = (Actor.Position - nextPosition).magnitude;
            totalMoveDistance -= moveDelta;

            Actor.Position = nextPosition;

            if (totalMoveDistance <= 0) break;
        }

        //Calculate Logical Directoin
        //Actor.transform.LookAt(destination);
    }

    public override void OnEnd()
    {
        
    }

    public override void Update()
    {
        if (_effectivePathPositions.Count == 0) return;

        for (; _finalPathIndex < _effectivePathPositions.Count; ++_finalPathIndex)
        {
            Vector3 destination = _effectivePathPositions[_finalPathIndex];
            if (destination == Actor.transform.position) continue;

            Actor.transform.position = Vector3.MoveTowards(Actor.transform.position, destination, Actor.MovingSpeed * Time.deltaTime);
            Actor.transform.LookAt(destination);
            break;
        }

        //if (Actor.OwnerShip == Ownership.OurForce)
        //{
        //    for (int i = 0; i < _totalPathPositions.Count - 1; ++i)
        //    {
        //        Debug.DrawLine(_totalPathPositions[i] + new Vector3(0f, 0.2f, 0f), _totalPathPositions[i + 1] + new Vector3(0f, 0.2f, 0f), Color.red);
        //    }

        //    Debug.DrawRay(Actor.Position, Vector3.up, Color.white);
        //}
    }
    
}


public enum MovementType
{
    Invalid     = 0x00,
    Normal      = 0x01,
    Flying      = 0x02,
}