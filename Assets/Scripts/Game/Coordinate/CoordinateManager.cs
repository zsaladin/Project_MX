using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
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

        public void UnregisterForPathFinder(BattleActor actor)
        {
            _actorPathFinders.Remove(actor);
        }

        public List<Vector3> GetPath(BattleActor actor)
        {
            return _actorPathFinders[actor].GetPath();
        }

        public void OnTick()
        {
            foreach (var pair in _actorPathFinders)
            {
                if (pair.Key.StateMachine.CurrentState.FindAction<BattleAction_Movement>() != null)
                    pair.Value.OnTick();
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
}