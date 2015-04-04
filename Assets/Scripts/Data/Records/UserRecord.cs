using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class UserRecord : Record
    {
        public List<ActorRecord> ActorRecords = new List<ActorRecord>();
    }
}