using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public struct GraphNode : System.IEquatable<GraphNode>
{
    public int X { get; private set; }
    public int Z { get; private set; }
    public float Height { get; private set; }

    private float _graphSqrMagnitude;
    public float GraphSqrMagnitude
    {
        get
        {
            if (_graphSqrMagnitude < 0)
                _graphSqrMagnitude = X * X + Z * Z;
            return _graphSqrMagnitude;
        }
    }

    private float _graphMagnitude;
    public float GraphMagnitude
    {
        get
        {
            if (_graphMagnitude < 0)
                _graphMagnitude = Mathf.Sqrt(GraphSqrMagnitude);
            return _graphMagnitude;
        }
    }

    private float _sqrMagnitude;
    public float SqrMagnitude
    {
        get
        {
            if (_sqrMagnitude < 0)
                _sqrMagnitude = X * X + Z * Z + Height * Height;
            return _sqrMagnitude;
        }
    }

    private float _magnitude;
    public float Magnitude
    {
        get
        {
            if (_magnitude < 0)
                _magnitude = Mathf.Sqrt(SqrMagnitude);
            return _magnitude;
        }
    }

    public GraphNode(int x, int z, float height)
        : this()
    {
        X = x;
        Z = z;
        Height = height;

        _graphMagnitude = float.MinValue;
        _graphSqrMagnitude = float.MinValue;
        _magnitude = float.MinValue;
        _sqrMagnitude = float.MinValue;
    }

    public bool Equals(GraphNode obj)
    {
        return (this.X == obj.X) && (this.Z == obj.Z) && (this.Height == obj.Height);
    }

    public override bool Equals(object obj)
    {
        return Equals((GraphNode)obj);
    }

    // todo: resoleve this overriden method.
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("X : {0}, Z : {1}, Height {2}", X, Z, Height);
    }
    public static bool operator == (GraphNode first, GraphNode second)
    {
        return first.Equals(second);
    }

    public static bool operator != (GraphNode first, GraphNode second)
    {
        return !first.Equals(second);
    }
}