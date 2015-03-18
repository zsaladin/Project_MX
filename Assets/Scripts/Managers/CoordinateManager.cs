using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CoordinateManager : MonoBehaviour 
{
    public bool _showGraph = false;

    public float _xMin = -10f;
    public float _xMax = 10f;
    public float _zMin = -10f;
    public float _zMax = 10f;
    public float _interval = 1f;

    private float _multiplier;
    private GraphNode[, ] _nodes;

    void Update()
    {
        if (_showGraph)
        {
            for (float x = _xMin; x < _xMax; x += _interval)
            {
                for (float z = _zMin; z < _zMax; z += _interval)
                {
                    GraphNode? fromNode = GetGraphNodeFromWorldCoord(x, z);
                    Vector3 from = GetVectorFromGraphNode(fromNode.Value.X, fromNode.Value.Z);

                    GraphNode? toNode = GetGraphNodeFromWorldCoord(x + _interval, z);
                    Vector3 to;
                    if (toNode != null)
                    {
                        to = GetVectorFromGraphNode(toNode.Value.X, toNode.Value.Z);
                        Debug.DrawLine(from, to);
                    }

                    toNode = GetGraphNodeFromWorldCoord(x, z + _interval);
                    if (toNode != null)
                    {
                        to = GetVectorFromGraphNode(toNode.Value.X, toNode.Value.Z);
                        Debug.DrawLine(from, to);
                    }
                }
            }
        }
    }

    public void Init()
    {
        _multiplier = 1 / _interval;
        _nodes = new GraphNode[(int)((_xMax - _xMin) / _interval + 1), (int)((_zMax - _zMin) / _interval + 1)];

        int layers = LayerMask.GetMask("Ground", "Arrangement");
        for(float x = _xMin; x < _xMax; x += _interval)
        {
            for (float z = _zMin; z < _zMax; z += _interval)
            {
                int posX = GetCoordinateX(x);
                int posZ = GetCoordivateZ(z);
                int height = int.MinValue;

                Ray ray = new Ray(new Vector3(x, 100, z), Vector3.down);
                RaycastHit[] hits = Physics.RaycastAll(ray, 500, layers);
                if (hits.Length > 0)
                {
                    RaycastHit hit = hits[0];
                    height = (int)hit.point.y;
                }
                GraphNode node = new GraphNode(posX, posZ, height);
                _nodes[posX, posZ] = node;
            }
        }
    }

    public int GetCoordinateX(float value)
    {
        return (int)((value + ((_xMax - _xMin) * 0.5f)) * _multiplier);
    }

    public int GetCoordivateZ(float value)
    {
        return (int)((value + ((_zMax - _zMin) * 0.5f)) * _multiplier);
    }
   
    public float GetWorldX(int value)
    {
        return (_interval * value - (_xMax - _xMin) * 0.5f);
    }

    public float GetWorldZ(int value)
    {
        return (_interval * value - (_zMax - _zMin) * 0.5f);
    }

    public GraphNode? GetGraphNodeFromWorldCoord(float x, float z)
    {
        int coordX = GetCoordinateX(x);
        int coordZ = GetCoordivateZ(z);
        if (coordX < 0 || coordX >= _nodes.GetLength(0)) return null;
        if (coordZ < 0 || coordZ >= _nodes.GetLength(0)) return null;

        return _nodes[coordX, coordZ];
    }

    public Vector3 GetVectorFromGraphNode(int x, int z)
    {
        GraphNode node = _nodes[x, z];
        return new Vector3(GetWorldX(node.X), node.Height, GetWorldZ(node.Z));
    }
}
