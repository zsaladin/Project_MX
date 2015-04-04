using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class ActorTypeProfileSave : ScriptableObject
    {
        public List<ActorTypeProfile> ActorTypeProfiles = new List<ActorTypeProfile>();

        public ActorTypeProfile Get(int id)
        {
            return ActorTypeProfiles.Find(item => item.ID == id);
        }
    }
}