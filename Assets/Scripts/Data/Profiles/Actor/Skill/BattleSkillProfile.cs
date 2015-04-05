using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleSkillProfile : Profile
    {
        public SkillType Type;
        public ParamData Params = new ParamData();
        public List<BattleSkillConditionProfile> Conditions = new List<BattleSkillConditionProfile>();
        public List<BattleBuffProfile> Buffs = new List<BattleBuffProfile>();

        public List<BattleEffectProfile> OnSkillEffects = new List<BattleEffectProfile>();
        public List<BattleEffectProfile> OnHitEffects = new List<BattleEffectProfile>();
        public List<BattleEffectProfile> OnSpotEffects = new List<BattleEffectProfile>();
    }
}
