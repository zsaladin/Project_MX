using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffMachine
    {
        private BattleActor _actor;
        private List<BattleBuff> _addedBuffs = new List<BattleBuff>();
        private List<BattleBuff> _removedBuffs = new List<BattleBuff>();
        private List<BattleBuff> _updateBuffs = new List<BattleBuff>();

        public List<BattleBuff> Buffs { get; private set; }

        public BattleBuffMachine(BattleActor actor)
        {
            _actor = actor;
            Buffs = new List<BattleBuff>();
        }

        public void OnTick()
        {
            for (int i = 0; i < _addedBuffs.Count; ++i)
            {
                _addedBuffs[i].OnBegin();
                Buffs.Add(_addedBuffs[i]);
            }
            _addedBuffs.Clear();


            for (int i = 0; i < Buffs.Count; ++i)
            {
                Buffs[i].OnTick();
                if (Buffs[i].IsEnded)
                {
                    Buffs[i].OnEnd();
                    _removedBuffs.Add(Buffs[i]);
                }
            }

            _updateBuffs.Clear();
            _updateBuffs.AddRange(_removedBuffs);

            Buffs.RemoveAll(item => _removedBuffs.Contains(item));
            _removedBuffs.Clear();
        }

        public void Update()
        {
            for (int i = 0; i < Buffs.Count; ++i)
                Buffs[i].Update();

            for (int i = 0; i < _updateBuffs.Count; ++i)
                _updateBuffs[i].Update();
        }

        public void AddBuff(BattleBuff buff)
        {
            _addedBuffs.Add(buff);
        }

        public void AddBuff(BattleBuffProfile buffProfile, BattleActor attacker)
        {
            AddBuff(new BattleBuff(buffProfile, _actor, attacker));
        }

        public bool ContainsBuffAction(BuffActionType type)
        {
            return Buffs.Find(item => item.ContainsAction(type)) != null;
        }


        public float GetBuffedMovementSpeed()
        {
            float value = 0;
            float ratio = 0;

            for(int i = 0; i < Buffs.Count; ++i)
            {
                BattleBuff buff = Buffs[i];
                var buffActionIterator = buff.FindActions<BattleBuffAction_MovementSpeed>();
                while(buffActionIterator.MoveNext())
                {
                    BattleBuffAction_MovementSpeed action = buffActionIterator.Current;
                    if (action.RatioValueType == RatioValueType.Value)
                        value += action.Value;
                    else
                        ratio += action.Value;
                }
            }

            return _actor.Property.RawMovingSpeed * (1 + ratio) + value;
        }
    }
}
