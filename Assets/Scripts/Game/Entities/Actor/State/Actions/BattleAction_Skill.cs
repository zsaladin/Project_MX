using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Skill : BattleAction
    {
        private BattleSkill _selectedSkill;
        private List<BattleSkill> _availabeSkills = new List<BattleSkill>();

        protected BattleActor _target;

        protected float _currentDuratiion;
        protected bool _isDealed;

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

            _availabeSkills.Clear();
            for (int i = 0; i < Actor.SkillMachine.Skills.Length; ++i)
            {
                if (Actor.SkillMachine.Skills[i].IsInCondition())
                    _availabeSkills.Add(Actor.SkillMachine.Skills[i]);
            }

            if (_availabeSkills.Count == 0)
            {
                IsEnd = true;
                return;
            }



            _selectedSkill = _availabeSkills[Random.Range(0, _availabeSkills.Count)];

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
                    _selectedSkill.OnSkill();
                }
            }
        }

        public override void Update()
        {
            if (_target != null)
                Actor.transform.LookAt(_target.transform);
        }
    }
}