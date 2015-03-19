using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntityManager : MonoBehaviour, ITickable
{
    public GameObject _rootOfOurUnits;
    public GameObject _rootOfOurBuildings;
    public GameObject _rootOfEnemyUnits;
    public GameObject _rootOfEnemyBuildings;

    public List<BattleActor> BattleActors { get; private set; }
    private List<BattleActor>[] _battleActorsByOwnership;

    public void Init()
    {
        BattleActors = new List<BattleActor>();

        _battleActorsByOwnership = new List<BattleActor>[System.Enum.GetNames(typeof(Ownership)).Length];
        _battleActorsByOwnership[(int)Ownership.OurForce] = new List<BattleActor>();
        _battleActorsByOwnership[(int)Ownership.EnemyForce] = new List<BattleActor>();
    }

    public List<BattleActor> GetActors(Ownership ownership, bool isClone)
    {
        if (isClone == false)
            return _battleActorsByOwnership[(int)ownership];

        return new List<BattleActor>(_battleActorsByOwnership[(int)ownership]);
    }


    public BattleActor CreateActors(Ownership ownership, ActorRecord actorRecord, Vector3? position = null)
    {
        ActorProfile profile = Manager.Data.ActorProfileSave.Get(actorRecord.ProfileID);
        GameObject obj = Instantiate<GameObject>(profile.Prefab);
        BattleActor battleActor = obj.AddComponent<BattleActor>();
        battleActor.Init(actorRecord, ownership, position);

        if (ownership == Ownership.OurForce)
            battleActor.transform.parent = _rootOfOurUnits.transform;
        else
            battleActor.transform.parent = _rootOfEnemyUnits.transform;

        Manager.Coordinate.RegisterForPathFinder(battleActor);
        _battleActorsByOwnership[(int)ownership].Add(battleActor);
        BattleActors.Add(battleActor);
        
        return battleActor;
    }

    public void CreateUser(Ownership ownership, UserRecord userRecord)
    {
        for (int i = 0; i < userRecord.ActorRecords.Count; ++i)
            CreateActors(ownership, userRecord.ActorRecords[i]);
    }

    public void OnTick()
    {
        int length = BattleActors.Count;
        for(int i = 0; i < length; ++i)
        {
            BattleActors[i].OnTick();
        }
    }
}

public enum Ownership
{
    OurForce = 0,
    EnemyForce = 1,
}