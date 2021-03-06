﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class DataManager
    {
        public ActorProfileSave ActorProfileSave { get; private set; }
        public ActorTypeProfileSave ActorTypeProfileSave { get; private set; }
        public ProjectileProfileSave ProjectileProfileSave { get; private set; }
        public UserRecordSave UserRecordSave { get; private set; }


        public void Init()
        {
            ActorProfileSave = ScriptableObjectUtility.GetAsset<ActorProfileSave>(DataType.Profile);
            ActorTypeProfileSave = ScriptableObjectUtility.GetAsset<ActorTypeProfileSave>(DataType.Profile);
            ProjectileProfileSave = ScriptableObjectUtility.GetAsset<ProjectileProfileSave>(DataType.Profile);

            UserRecordSave = ScriptableObjectUtility.GetAsset<UserRecordSave>(DataType.Record);
        }
    }
}