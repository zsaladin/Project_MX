using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CoordinateManager : MonoBehaviour, ITickable
{
    public bool _showGraph = false;
    public Graph _graph = new Graph();
    public Graph Graph { get { return _graph; } }

    public Dictionary<BattleActor, PathFinder> _actorPathFinders = new Dictionary<BattleActor, PathFinder>();

    public void Init()
    {
        _graph.Init();
    }

    public void RegisterForPathFinder(BattleActor actor)
    {
        _actorPathFinders.Add(actor, new PathFinder(actor));
    }

    public void OnTick()
    {
        foreach(PathFinder finder in _actorPathFinders.Values)
        {
            
        }
    }

    void OnDrawGizmos()
    {
        if (_showGraph)
        {
            _graph.Show();
        }
    }
}
