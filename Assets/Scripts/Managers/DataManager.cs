using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataManager
{
    public ActorProfileSave ActorProfileSave { get; private set; }
    public ActorTypeProfileSave ActorTypeProfileSave { get; private set; }
    public ConditionProfileSave ConditionProfileSave { get; private set; }
    public UserRecordSave UserRecordSave { get; private set; }


    public void Init()
    {
        ActorProfileSave = ScriptableObjectUtility.GetAsset<ActorProfileSave>(DataType.Profile);
        ActorTypeProfileSave = ScriptableObjectUtility.GetAsset<ActorTypeProfileSave>(DataType.Profile);
        ConditionProfileSave = ScriptableObjectUtility.GetAsset<ConditionProfileSave>(DataType.Profile);

        UserRecordSave = ScriptableObjectUtility.GetAsset<UserRecordSave>(DataType.Record);
    }
}
