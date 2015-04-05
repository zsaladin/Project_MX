using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleBuffProfile : Profile
    {
        public List<BattleBuffConditionProfile> Conditions = new List<BattleBuffConditionProfile>();
        public List<BattleBuffActionProfile> Actions = new List<BattleBuffActionProfile>();

        public List<BattleEffectProfile> Effects = new List<BattleEffectProfile>();
    }
}