using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntityManager : MonoBehaviour 
{
    public GameObject _rootOfOurUnits;
    public GameObject _rootOfOurBuildings;
    public GameObject _rootOfEnemyUnits;
    public GameObject _rootOfEnemyBuildings;

    private List<BattleActor> _battleObjects;
    private List<BattleActor>[] _battleObjectsByOwnership;

    public void Init()
    {
        _battleObjects = new List<BattleActor>();

        _battleObjectsByOwnership = new List<BattleActor>[System.Enum.GetNames(typeof(Ownership)).Length];
        _battleObjectsByOwnership[(int)Ownership.OurForce] = new List<BattleActor>();
        _battleObjectsByOwnership[(int)Ownership.EnemyForce] = new List<BattleActor>();
    }

    public List<BattleActor> GetActors(Ownership ownership, bool isClone)
    {
        if (isClone == false)
            return _battleObjectsByOwnership[(int)ownership];

        return new List<BattleActor>(_battleObjectsByOwnership[(int)ownership]);
    }


    public BattleActor CreateActors(Ownership ownership, ActorRecord actorRecord, Vector3? position = null)
    {
        ActorProfile profile = Manager.Data.ActorProfileSave.Get(actorRecord.ProfileID);
        GameObject obj = Instantiate<GameObject>(profile.Prefab);
        BattleActor battleObject = obj.AddComponent<BattleActor>();
        battleObject.Init(actorRecord, ownership, position);

        if (ownership == Ownership.OurForce)
            battleObject.transform.parent = _rootOfOurUnits.transform;
        else
            battleObject.transform.parent = _rootOfEnemyUnits.transform;

        _battleObjects.Add(battleObject);
        _battleObjectsByOwnership[(int)ownership].Add(battleObject);
        return battleObject;
    }

    public void CreateUser(Ownership ownership, UserRecord userRecord)
    {
        for (int i = 0; i < userRecord.ActorRecords.Count; ++i)
            CreateActors(ownership, userRecord.ActorRecords[i]);
    }

    public void OnTick()
    {
        int length = _battleObjects.Count;
        for(int i = 0; i < length; ++i)
        {
            _battleObjects[i].OnTick();
        }
    }
}

public enum Ownership
{
    OurForce = 0,
    EnemyForce = 1,
}