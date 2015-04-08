using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
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

        public BattleAction_Movement(BattleActor actor)
            : base(actor)
        {

        }

        public override void OnBegin()
        {
            base.OnBegin();

            Actor.AnimationController.Play(AnimationType.Walk, Actor.Property.MovingSpeed);
        }

        public override void OnTick()
        {
            _totalPathPositions = Manager.Coordinate.GetPath(Actor);
            _effectivePathPositions.Clear();
            _finalPathIndex = 0;

            if (_totalPathPositions == null) return;
            if (_totalPathPositions.Count == 0) return;

            float totalMoveDistance = Actor.Property.MovingSpeed * Manager.Constant.GAME_TICK;

            for (int i = 1; i <= _totalPathPositions.Count; ++i)
            {
                Vector3 destination = _totalPathPositions[_totalPathPositions.Count - i];
                _effectivePathPositions.Add(destination);

                Vector3 nextPosition = Vector3.MoveTowards(Actor.Position, destination, totalMoveDistance);
                float moveDelta = (Actor.Position - nextPosition).magnitude;
                totalMoveDistance -= moveDelta;

                Actor.Position = nextPosition;

                if (totalMoveDistance <= 0)
                {
                    Actor.BaseAction.LookAt(destination);
                    break;
                }
            }



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

                Actor.transform.position = Vector3.MoveTowards(Actor.transform.position, destination, Actor.Property.MovingSpeed * Time.deltaTime);
                destination.y = Actor.transform.position.y;
                Actor.transform.LookAt(destination);
                break;
            }
        }

    }


    public enum MovementType
    {
        Invalid = 0x00,
        Normal = 0x01,
        Flying = 0x02,
    }
}