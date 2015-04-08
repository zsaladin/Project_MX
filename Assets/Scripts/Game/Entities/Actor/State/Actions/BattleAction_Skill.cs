using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Skill : BattleAction
    {
        private BattleSkill _selectedSkill;
        private BattleActor _target;

        private float _currentDuratiion;
        private bool _isDealed;

        public override ActionType Type
        {
            get
            {
                return ActionType.Skill;
            }
        }

        public BattleAction_Skill(BattleActor actor)
            : base(actor)
        {

        }

        public override void OnBegin()
        {
            base.OnBegin();

            _target = Actor.Target;
            if (_target == null)
            {
                IsEnd = true;
                return;
            }

            _selectedSkill = Actor.SkillMachine.NextSkill;
            if (_selectedSkill == null)
            {
                IsEnd = true;
                return;
            }

            _currentDuratiion = 0;
            _isDealed = false;

            Actor.AnimationController.Play(AnimationType.Attack);

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
                    _selectedSkill.OnSkill();
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
    }
}