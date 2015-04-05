using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ActorProfileSave : ScriptableObject
    {
        public List<BattleActorProfile> ActorProfiles = new List<BattleActorProfile>();

        public BattleActorProfile Get(int id)
        {
            return ActorProfiles.Find(item => item.ID == id);
        }
    }
}