using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleActorProfile : Profile
    {
        public GameObject Prefab;
        public int ActorType;

        public float Size;
        public float HitPointMax;

        public OffenseType OffenseType;
        public int OffenseProjectileType;

        public float OffensePower;
        public float OffenseTime;
        public float OffenseDealTime;
        public float OffenseRange;

        public DefenseType DefenseType;

        public float MovingSpeed;

        public List<BattleSkillProfile> Skills = new List<BattleSkillProfile>();

        public List<BattleEffectProfile> OnDealEffects = new List<BattleEffectProfile>();
        public List<BattleEffectProfile> OnHitEffects = new List<BattleEffectProfile>();
        public List<BattleEffectProfile> OnSpotEffects = new List<BattleEffectProfile>();
    }
}
