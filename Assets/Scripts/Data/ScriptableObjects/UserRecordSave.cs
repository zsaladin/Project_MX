using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class UserRecordSave : ScriptableObject
    {
        public List<UserRecord> UserRecords = new List<UserRecord>();

        public UserRecord Get(int id)
        {
            return UserRecords.Find(item => item.ID == id);
        }
    }
}