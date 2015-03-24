using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntityManager : MonoBehaviour, ITickable
{
    public GameObject _rootOfOurUnits;
    public GameObject _rootOfOurBuildings;
    public GameObject _rootOfEnemyUnits;
    public GameObject _rootOfEnemyBuildings;

    private List<BattleActor> _battleActors;
    private List<BattleActor>[] _battleActorsByOwnership;

    private List<BattleActor> _removeActors;

    private List<BattleProjectile> _battleProjectiles;
    private List<BattleProjectile> _removeProjectiles;

    public void Init()
    {
        _battleActors = new List<BattleActor>();

        _battleActorsByOwnership = new List<BattleActor>[System.Enum.GetNames(typeof(Ownership)).Length];
        _battleActorsByOwnership[(int)Ownership.OurForce] = new List<BattleActor>();
        _battleActorsByOwnership[(int)Ownership.EnemyForce] = new List<BattleActor>();

        _removeActors = new List<BattleActor>();

        _battleProjectiles = new List<BattleProjectile>();
        _removeProjectiles = new List<BattleProjectile>();
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
        _battleActors.Add(battleActor);
        
        return battleActor;
    }

    public BattleProjectile CreateProjectile(ProjectileProfile projectileProfile, Vector3 position)
    {
        GameObject obj = GameObject.Instantiate<GameObject>(projectileProfile.Prefab);
        BattleProjectile projectile = obj.AddComponent<BattleProjectile>();
        projectile.Init(projectileProfile, position);

        _battleProjectiles.Add(projectile);
        return projectile;
    }

    public void OnTick()
    {
        for(int i = 0; i < _battleActors.Count; ++i)
        {
            _battleActors[i].OnTick();
        }

        for(int i = 0; i < _battleProjectiles.Count; ++i)
        {
            _battleProjectiles[i].OnTick();
        }
    }

    public void OnPostTick()
    {
        for (int i = 0; i < _removeActors.Count; ++i )
        {
            BattleActor actor = _removeActors[i];
            _battleActors.Remove(actor);
            _battleActorsByOwnership[(int)actor.OwnerShip].Remove(actor);

            actor.EnableUIs(false);

            Manager.Coordinate.UnregisterForPathFinder(actor);
        }

        for (int i = 0; i < _removeProjectiles.Count; ++i)
        {
            BattleProjectile projectile = _removeProjectiles[i];
            _battleProjectiles.Remove(projectile);
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

    public void RemoveProjectile(BattleProjectile projectile)
    {
        _removeProjectiles.Add(projectile);
    }
}

public enum Ownership
{
    OurForce = 0,
    EnemyForce = 1,
}