using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ActorProfileSave : ScriptableObject 
{
    public List<ActorProfile> ActorProfiles = new List<ActorProfile>();

    public ActorProfile Get(int id)
    {
        return ActorProfiles.Find(item => item.ID == id);
    }
}
