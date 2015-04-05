using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class ActorTypeProfileSave : ScriptableObject
    {
        public List<BattleActorTypeProfile> ActorTypeProfiles = new List<BattleActorTypeProfile>();

        public BattleActorTypeProfile Get(int id)
        {
            return ActorTypeProfiles.Find(item => item.ID == id);
        }
    }
}