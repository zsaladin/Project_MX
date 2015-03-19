using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinder : ITickable
{
    private BattleActor _actor;

    private PathNode[] _nodes;
    private PathNode _currentNode;
    
    private SortedList<int, PathNode> _openedNodes = new SortedList<int,PathNode>();
    private List<PathNode> _closedNodes = new List<PathNode>();

    private Direction[] _directions;

    public PathFinder(BattleActor actor)
    {
        _actor = actor;
        _directions = System.Enum.GetValues(typeof(Direction)) as Direction[];
        System.Array.Copy(_directions, 1, _directions, 0, _directions.Length - 1);

        InitNodes();
    }

    void InitNodes()
    {
        _nodes = new PathNode[Graph.XCount * Graph.ZCount];

        for (int x = 0; x < Graph.XCount; ++x)
        {
            for (int z = 0; z < Graph.ZCount; ++z)
            {
                _nodes[x * Graph.ZCount + z] = new PathNode(Manager.Coordinate.Graph.GetNode(x, z).Value);
            }
        }
    }

    public PathNode GetNode(int x, int z)
    {
        if (x < 0 || z < 0 || x >= Graph.XCount || z >= Graph.ZCount) return null;

        return _nodes[x * Graph.ZCount + z];
    }

    public PathNode GetNodeFromVector(Vector3 position)
    {
        return GetNodeFromVector(position.x, position.z);
    }

    public PathNode GetNodeFromVector(float x, float z)
    {
        Vector3 actorPosition = _actor.transform.position;
        int coordX = Graph.GetCoordinateX(actorPosition.x);
        int coordZ = Graph.GetCoordivateZ(actorPosition.z);

        return GetNode(coordX, coordZ);
    }

    public void OnTick()
    {
        _openedNodes.Clear();
        _closedNodes.Clear();

        _currentNode = GetNodeFromVector(_actor.transform.position);

        
        //for(Direction dir = (Direction)
        //_openedNodes.Add()


    }

}
