using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class BattleEffectProfile : Profile
    {
        public GameObject Prefab;

        public float Delay;
        public float Duration;
        public bool IsLooping;

        public bool IsGlobal;
        public EffectNodeType NodeType;
    }
}