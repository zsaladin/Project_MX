using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleBuffMachine : ITickable
    {
        private BattleActor _actor;
        private List<BattleBuff> _addedBuffs = new List<BattleBuff>();
        private List<BattleBuff> _removedBuffs = new List<BattleBuff>();

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

            Buffs.RemoveAll(item => _removedBuffs.Contains(item));
            _removedBuffs.Clear();
        }

        public void AddBuff(BattleBuff buff)
        {
            _addedBuffs.Add(buff);
        }

        public void AddBuff(BattleBuffProfile buffProfile)
        {
            AddBuff(new BattleBuff(buffProfile, _actor));
        }

        public bool ContainsBuff(BuffActionType type)
        {
            return Buffs.Find(item => item.ContainsAction(type)) != null;
        }
    }
}
