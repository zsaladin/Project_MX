using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ActorTypeProfile : Profile
{
    /// <summary>
    /// State Profile ID
    /// </summary>
    public List<BattleStateProfile> States = new List<BattleStateProfile>();

    public BattleStateProfile GetStateProfile(int id)
    {
        return States.Find(item => item.ID == id);
    }
}
