using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UserRecord : Record 
{
    public List<ActorRecord> ActorRecords = new List<ActorRecord>();
}
