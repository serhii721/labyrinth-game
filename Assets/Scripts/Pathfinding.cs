using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }
    
    private int gridSizeX;
    private int gridSizeY;
    private Node[,] grid;
    private List<Node> openSet;       // Open set for nodes to be calculated
    private HashSet<Node> closedSet;  // Closed set for already calculated nodes

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroying duplicate
        }
        else
        {
            Instance = this; // Keeping a single copy
        }
    }

    // Method for path finding
    public List<Node> FindPath(Vector2Int startPos, Vector2Int targetPos, bool[,] mazeGrid)
    {
        // Tranforming grid into nodes
        gridSizeX = mazeGrid.GetLength(1);
        gridSizeY = mazeGrid.GetLength(0);
        grid = new Node[gridSizeX, gridSizeY];

        // Filling nodes
        for (int i = 0; i < gridSizeX; ++i)
            for (int j = 0; j < gridSizeY; ++j)
                grid[i, j] = new Node(new Vector2Int(i, j), !mazeGrid[i, j]);

        // Interpreting coordinates on grid
        Node startNode = grid[startPos.x, startPos.y];
        Node targetNode = grid[targetPos.x, targetPos.y];

        // Initializing sets
        openSet = new List<Node>();
        closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Choosing most valuable node
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    currentNode = openSet[i];
            }

            // Updating sets
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // If path is found - return it
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            // Calculating neighbors' cost
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return null;  // Path is not found
    }

    // Method for getting neighbouring nodes
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0) || x == y || x == -y)
                    continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbors.Add(grid[checkX, checkY]);
            }
        }

        return neighbors;
    }

    // Method for calculating distance between two nodes (manhattan distance)
    private float GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int dstY = Mathf.Abs(nodeA.position.y - nodeB.position.y);
        return dstX + dstY;
    }

    // Method for path retracing
    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
}
