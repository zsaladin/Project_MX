using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum Direction
{
    Invalid = 0x00,
    Top = 0x01,
    Bottom = 0x02,
    Left = 0x04,
    Right = 0x08,
    TopLeft = Top | Left,
    TopRight = Top | Right,
    BottomLeft = Bottom | Left,
    BottomRight = Bottom | Right,
}

[System.Serializable]
public class Graph 
{
    public float _xMin = -10f;
    public float _xMax = 10f;
    public float _zMin = -10f;
    public float _zMax = 10f;
    public static float _interval = 1f;
    public static float _multiplier;

    public static int XCount { get; private set; }
    public static int ZCount { get; private set; }

    public static int XLength { get; private set; }
    public static int ZLength { get; private set; }

    private GraphNode[] _nodes;

    public void Init()
    {
        _multiplier = 1 / _interval;

        XLength = Mathf.RoundToInt(_xMax - _xMin);
        ZLength = Mathf.RoundToInt(_zMax - _zMin);

        XCount = Mathf.RoundToInt(XLength * _multiplier);
        ZCount = Mathf.RoundToInt(ZLength * _multiplier);

        _nodes = new GraphNode[XCount * ZCount];

        int layers = LayerMask.GetMask("Ground", "Arrangement");
        for(int x = 0; x < XCount; ++x)
        {
            for(int z = 0; z < ZCount; ++z)
            {
                float worldX = GetWorldX(x);
                float worldZ = GetWorldZ(z);
                float height = float.MinValue;

                Ray ray = new Ray(new Vector3(worldX, 100, worldZ), Vector3.down);
                RaycastHit[] hits = Physics.RaycastAll(ray, 500, layers);
                if (hits.Length > 0)
                {
                    RaycastHit hit = hits.Aggregate((first, second) => first.point.y > second.point.y ? first : second);
                    height = hit.point.y;
                }

                GraphNode node = new GraphNode(x, z, height);
                _nodes[x * ZCount + z] = node;
            }
        }
    }

    public static int GetCoordinateX(float worldX)
    {
        return (int)((worldX + XLength * 0.5f) * _multiplier);
    }

    public static int GetCoordivateZ(float worldZ)
    {
        return (int)((worldZ + ZLength * 0.5f) * _multiplier);
    }

    public static float GetWorldX(int coordX)
    {
        return (_interval * coordX - XLength * 0.5f);
    }

    public static float GetWorldZ(int coordZ)
    {
        return (_interval * coordZ - ZLength * 0.5f);
    }

    public static int GetNearCoordinateX(int x, Direction direction)
    {
        if (EnumHas(direction, Direction.Top))
            --x;
        else if (EnumHas(direction, Direction.Bottom))
            ++x;

        return x;
    }

    public static int GetNearCoordinateZ(int z, Direction direction)
    {
        if (EnumHas(direction, Direction.Left))
            --z;
        else if (EnumHas(direction, Direction.Right))
            ++z;

        return z;
    }

    // Extension Method 'Has' have performance issue;
    static bool EnumHas(Direction enumFirst, Direction enumSecond)
    {
        int first = System.Convert.ToInt32(enumFirst);
        int second = System.Convert.ToInt32(enumSecond);

        return (first & second) == second;
    }

    public GraphNode? GetNode(int x, int z)
    {
        if (x < 0 || z < 0 || x >= XCount || z >= ZCount) return null;

        return _nodes[x * ZCount + z];
    }

    public GraphNode? GetNodeFromWorldCoord(Vector3 position)
    {
        return GetNodeFromWorldCoord(position.x, position.z);
    }

    public GraphNode? GetNodeFromWorldCoord(float x, float z)
    {
        int coordX = GetCoordinateX(x);
        int coordZ = GetCoordivateZ(z);

        return GetNode(coordX, coordZ);
    }

    public Vector3? GetVectorFromNode(GraphNode node)
    {
        return GetVectorFromNode(node.X, node.Z);
    }

    public Vector3? GetVectorFromNode(int x, int z)
    {
        GraphNode? node = GetNode(x, z);
        if (node == null) return null;

        return new Vector3(GetWorldX(node.Value.X), node.Value.Height, GetWorldZ(node.Value.Z));
    }

    public GraphNode? GetNearNode(GraphNode node, Direction direction)
    {
        return GetNearNode(node.X, node.Z, direction);
    }

    public GraphNode? GetNearNode(int x, int z, Direction direction)
    {
        return GetNode(GetNearCoordinateX(x, direction), GetNearCoordinateX(z, direction));
    }

    public void Show()
    {
        for (int x = 0; x < XCount; ++x)
        {
            for (int z = 0; z < ZCount; ++z)
            {
                GraphNode? fromNode = GetNode(x, z);
                GraphNode? toNode = GetNode(x + 1, z);
                if (toNode != null)
                    DrawLine(fromNode.Value, toNode.Value);

                toNode = GetNode(x, z + 1);
                if (toNode != null)
                    DrawLine(fromNode.Value, toNode.Value);

                //Vector3 from = GetVectorFromNode(fromNode.Value.X, fromNode.Value.Z).Value;
                //UnityEditor.Handles.Label(from, string.Format("Node : {0}, Position : {1}", fromNode.Value.ToString(), from.ToString()));
                
            }
        }
    }

    void DrawLine(GraphNode from, GraphNode to)
    {
        Debug.DrawLine(GetVectorFromNode(from).Value, GetVectorFromNode(to).Value);
    }
}
