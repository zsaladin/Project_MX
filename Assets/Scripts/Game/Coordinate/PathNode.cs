using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathNode 
{
    public GraphNode GraphNode { get; private set; }
    public PathNode ParentPathNode { get; set; }

    public float F { get; set; }
    public float G { get; set; }
    public float H { get; set; }

    public PathNode(GraphNode node)
    {
        GraphNode = node;
    }
}
