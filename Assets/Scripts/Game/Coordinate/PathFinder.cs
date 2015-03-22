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
    private PathNode _leastCostNode;
    private PathNodeComparer _comparer;

    private HashSet<PathNode> _openedHash = new HashSet<PathNode>();
    //private List<PathNode> _openedNodes = new List<PathNode>();

    private HashSet<PathNode> _closedHash = new HashSet<PathNode>();
    //private List<PathNode> _closedNodes = new List<PathNode>();

    private List<Vector3> _pathPositions = new List<Vector3>();

    private Direction[] _directions;
    private bool _isSuccess;

    public PathFinder(BattleActor actor)
    {
        _actor = actor;
        _comparer = new PathNodeComparer();
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

    void InitNode(PathNode thisNode)
    {
        thisNode.ParentPathNode = null;
        thisNode.F = 0;
        thisNode.G = 0;
        thisNode.H = 0;
    }

    public void OnTick()
    {
        _openedHash.Clear();
        _closedHash.Clear();

        _currentNode = GetNodeFromVector(_actor.Position);
        _destinationNode = GetNodeFromVector(_actor.Destination);
        _calculatedDestNode = null;
        _leastCostNode = null;

        InitNode(_currentNode);
        PathNode thisNode = _currentNode;

        for (int i = 0; i < _directions.Length; ++i)
        {
            Direction dir = _directions[i];
            PathNode nearNode = GetNearNode(_currentNode, dir);
            if (nearNode == null) continue;

            nearNode.ParentPathNode = thisNode;
            AddToOpened(nearNode);
            CalculateCosts(nearNode);

            if (nearNode == _destinationNode)
            {
                _calculatedDestNode = _destinationNode;
                _isSuccess = true;
                break;
            }
        }

        if (_calculatedDestNode != null) return;

        int repeat = 0;
        const int maxRepeat = 3000;
        while (repeat < maxRepeat)
        {
            ++repeat;
            if (_openedHash.Count == 0) break;
            if (_openedHash.Contains(_destinationNode))
            {
                _calculatedDestNode = _destinationNode;
                _isSuccess = true;
                break;
            }

            if (_leastCostNode == null)
            {
                _leastCostNode = GetTheLeastCostNode();
            }
            thisNode = _leastCostNode;
            RemoveFromOpened(thisNode);

            _closedHash.Add(thisNode);

            for (int i = 0; i < _directions.Length; ++i)
            {
                Direction dir = _directions[i];
                PathNode nearNode = GetNearNode(thisNode, dir);
                if (nearNode == null) continue;

                if (_closedHash.Contains(nearNode)) continue;
                if (_openedHash.Contains(_destinationNode))
                {
                    // recalculate
                }
                else
                {
                    CalculateCosts(nearNode);

                    float diffHeight = Mathf.Abs(nearNode.GraphNode.Height - thisNode.GraphNode.Height);
                    if (diffHeight < 0.3f)
                    {
                        AddToOpened(nearNode);
                        nearNode.ParentPathNode = thisNode;
                    }
                    else
                    {
                        _closedHash.Add(nearNode);
                    }
                }
            }
        }
        if (repeat == maxRepeat)
        {
            //_calculatedDestNode = thisNode;
            _isSuccess = false;
        }
    }

    void AddToOpened(PathNode thisNode)
    {
        _openedHash.Add(thisNode);
        if (_leastCostNode == null)
        {
            if (_openedHash.Count == 0)
                _leastCostNode = thisNode;
        }
        else if (_leastCostNode.F > thisNode.F)
            _leastCostNode = thisNode;
    }

    void RemoveFromOpened(PathNode thisNode)
    {
        _openedHash.Remove(thisNode);
        if (_leastCostNode == thisNode)
            _leastCostNode = null;
    }

    PathNode GetTheLeastCostNode()
    {
        int minFCost = int.MaxValue;
        PathNode node = null;
        foreach (PathNode item in _openedHash)
        {
            if (minFCost > item.F)
            {
                minFCost = item.F;
                node = item;
            }
        }
        return node;
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

        return parentNode.G + CalculateCostBetweenNodeAndParent(thisNode, parentNode);
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

    int CalculateCostBetweenNodeAndParent(PathNode thisNode, PathNode parentNode)
    {
        if (thisNode.GraphNode.X == parentNode.GraphNode.X) return PathNode.STRAIGHT_LINE_COST;
        if (thisNode.GraphNode.Z == parentNode.GraphNode.Z) return PathNode.STRAIGHT_LINE_COST;
        return PathNode.DIAGONAL_LINE_COST;
    }

    public List<Vector3> GetPath()
    {
        if (_isSuccess == false) return _pathPositions;

        _pathPositions.Clear();
        PathNode thisNode = _calculatedDestNode;
        while (true)
        {
            if (thisNode == null) break;
            if (thisNode == _currentNode) break;
            if (_pathPositions.Contains(GetVectorFromNode(thisNode))) break;

            _pathPositions.Add(GetVectorFromNode(thisNode));
            thisNode = thisNode.ParentPathNode;
        }

        return _pathPositions;
    }

    class PathNodeComparer : IComparer<PathNode>
    {
        public int Compare(PathNode x, PathNode y)
        {
            return x.F - y.F;
        }
    }
}
