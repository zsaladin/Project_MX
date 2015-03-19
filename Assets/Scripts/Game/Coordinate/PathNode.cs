using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathNode 
{
    public static readonly int STRAIGHT_LINE_COST = 10;
    public static readonly int DIAGONAL_LINE_COST = 14;

    public GraphNode GraphNode { get; private set; }
    public PathNode ParentPathNode { get; set; }

    public int F { get; set; }
    public int G { get; set; }
    public int H { get; set; }

    public PathNode(GraphNode node)
    {
        GraphNode = node;
    }
}
