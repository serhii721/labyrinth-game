using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position;  // Cell position on the grid
    public bool walkable;        // Can the player walk through this cell
    public Node parent;          // Previous node
    public float gCost;          // Path cost from starting node
    public float hCost;          // Expected heuristic cost for end node
    public float fCost { get { return gCost + hCost; } }  // Total cost

    public Node(Vector2Int pos, bool passable)
    {
        position = pos;
        walkable = passable;
    }
}