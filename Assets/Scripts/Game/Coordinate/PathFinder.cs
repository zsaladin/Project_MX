using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinder : ITickable
{
    private BattleActor _actor;

    private PathNode[] _nodes;
    private PathNode _currentNode;
    private PathNode _destinationNode;
    private PathNode _calculatedDestNode;
    
    private List<PathNode> _openedNodes = new List<PathNode>();
    private List<PathNode> _closedNodes = new List<PathNode>();

    private List<Vector3> _pathPositions = new List<Vector3>();

    private Direction[] _directions;

    public PathFinder(BattleActor actor)
    {
        _actor = actor;
        _directions = System.Enum.GetValues(typeof(Direction)) as Direction[];

        var tempDirectinos = _directions.ToList();
        tempDirectinos.RemoveAt(0);
        _directions = tempDirectinos.ToArray();

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

    public Vector3 GetVectorFromNode(PathNode thisNode)
    {
        return new Vector3(Graph.GetWorldX(thisNode.GraphNode.X), thisNode.GraphNode.Height, Graph.GetWorldZ(thisNode.GraphNode.Z));
    }

    public PathNode GetNodeFromVector(Vector3 position)
    {
        return GetNodeFromVector(position.x, position.z);
    }

    public PathNode GetNodeFromVector(float x, float z)
    {
        int coordX = Graph.GetCoordinateX(x);
        int coordZ = Graph.GetCoordivateZ(z);

        return GetNode(coordX, coordZ);
    }

    public PathNode GetNearNode(PathNode node, Direction direction)
    {
        return GetNearNode(node.GraphNode.X, node.GraphNode.Z, direction);
    }

    public PathNode GetNearNode(int x, int z, Direction direction)
    {
        return GetNode(Graph.GetNearCoordinateX(x, direction), Graph.GetNearCoordinateZ(z, direction));
    }

    

    void CalculateCosts(PathNode thisNode)
    {
        thisNode.G = CalculateCostG(thisNode);
        thisNode.H = CalculateCostH(thisNode);
        thisNode.F = thisNode.G + thisNode.H;
    }

    int CalculateCostG(PathNode thisNode)
    {
        PathNode parentNode = thisNode.ParentPathNode;
        if (parentNode == null) return 0;


        return parentNode.G + CalculateCostBetweenNodes(thisNode, parentNode);
    }
    int CalculateCostH(PathNode thisNode)
    {
        return CalculateCostBetweenNodes(thisNode, _destinationNode);
    }

    int CalculateCostBetweenNodes(PathNode sourceNode, PathNode destNode)
    {
        GraphNode sourceGraphNode = sourceNode.GraphNode;
        GraphNode destGraphNode = destNode.GraphNode;

        int diffX = Mathf.Abs(sourceGraphNode.X - destGraphNode.X);
        int diffZ = Mathf.Abs(sourceGraphNode.Z - destGraphNode.Z);

        int diagonal = Mathf.Min(diffX, diffZ);
        int straight = Mathf.Abs(diffX - diffZ);

        return diagonal * PathNode.DIAGONAL_LINE_COST + straight * PathNode.STRAIGHT_LINE_COST;
    }

    void InitNode(PathNode thisNode)
    {
        thisNode.ParentPathNode = null;
        thisNode.F = 0;
        thisNode.G = 0;
        thisNode.H = 0;
    }

    public void OnTick()
    {
        _openedNodes.Clear();
        _closedNodes.Clear();

        _currentNode = GetNodeFromVector(_actor.transform.position);
        //_destinationNode = GetNodeFromVector(_actor.Destination);
        _destinationNode = GetNodeFromVector(Vector3.zero);

        InitNode(_currentNode);
        PathNode thisNode = _currentNode;

        for (int i = 0; i < _directions.Length; ++i)
        {
            Direction dir = _directions[i];
            PathNode nearNode = GetNearNode(_currentNode, dir);
            if (nearNode == null) continue;

            nearNode.ParentPathNode = thisNode;
            CalculateCosts(nearNode);
            _openedNodes.Add(nearNode);
        }

        while (true)
        {
            if (_openedNodes.Count == 0) break;
            if (_openedNodes.Contains(_destinationNode))
            {
                _calculatedDestNode = _destinationNode;
                break;
            }

            _openedNodes.Sort((first, second) => first.F -second.F);
            thisNode = _openedNodes[0];
            _openedNodes.RemoveAt(0);
            _closedNodes.Add(thisNode);

            for (int i = 0; i < _directions.Length; ++i)
            {
                Direction dir = _directions[i];
                PathNode nearNode = GetNearNode(thisNode, dir);
                if (nearNode == null) continue;

                if (_closedNodes.Contains(nearNode)) continue;
                if (_openedNodes.Contains(nearNode))
                {
                    // recalculate
                }
                else
                {
                    _openedNodes.Add(nearNode);
                    nearNode.ParentPathNode = thisNode;
                    CalculateCosts(nearNode);
                }
            }
        }
    }

    public List<Vector3> GetPath()
    {
        _pathPositions.Clear();
        PathNode thisNode = _calculatedDestNode;
        while(thisNode != _currentNode)
        {
            _pathPositions.Add(GetVectorFromNode(thisNode));
            thisNode = thisNode.ParentPathNode;
        }

        return _pathPositions;
    }

}
