using UnityEngine;
using System.Collections.Generic;

namespace MX
{
    [System.Serializable]
    public class BattleActorTypeProfile : Profile
    {
        public List<BattleStateProfile> States = new List<BattleStateProfile>();

        public BattleStateProfile GetStateProfile(int id)
        {
            return States.Find(item => item.ID == id);
        }
    }
}