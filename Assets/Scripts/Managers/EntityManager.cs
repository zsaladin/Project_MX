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

    private List<BattleActor> _removeActors;

    public void Init()
    {
        BattleActors = new List<BattleActor>();

        _battleActorsByOwnership = new List<BattleActor>[System.Enum.GetNames(typeof(Ownership)).Length];
        _battleActorsByOwnership[(int)Ownership.OurForce] = new List<BattleActor>();
        _battleActorsByOwnership[(int)Ownership.EnemyForce] = new List<BattleActor>();

        _removeActors = new List<BattleActor>();
    }

    public void CreateUser(Ownership ownership, UserRecord userRecord)
    {
        for (int i = 0; i < userRecord.ActorRecords.Count; ++i)
            CreateActors(ownership, userRecord.ActorRecords[i]);
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

        Manager.UI.InitActorSlider(battleActor);
        Manager.Coordinate.RegisterForPathFinder(battleActor);
        _battleActorsByOwnership[(int)ownership].Add(battleActor);
        BattleActors.Add(battleActor);
        
        return battleActor;
    }

    public void OnTick()
    {
        for(int i = 0; i < BattleActors.Count; ++i)
        {
            BattleActors[i].OnTick();
        }
    }

    public void OnPostTick()
    {
        for (int i = 0; i < _removeActors.Count; ++i )
        {
            BattleActor actor = _removeActors[i];
            BattleActors.Remove(actor);
            _battleActorsByOwnership[(int)actor.OwnerShip].Remove(actor);

            actor.EnableUIs(false);

            Manager.Coordinate.UnregisterForPathFinder(actor);
        }
            
    }

    public List<BattleActor> GetActors(Ownership ownership, bool isClone)
    {
        if (isClone == false)
            return _battleActorsByOwnership[(int)ownership];

        return new List<BattleActor>(_battleActorsByOwnership[(int)ownership]);
    }

    public void RemoveActor(BattleActor actor)
    {
        _removeActors.Add(actor);
    }
}

public enum Ownership
{
    OurForce = 0,
    EnemyForce = 1,
}