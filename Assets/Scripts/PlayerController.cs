using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private List<Node> path;

    void Start()
    {
        Invoke("StartMoving", 2f); // 2 seconds delay before movement
    }

    void StartMoving()
    {
        // Getting labyrinth
        bool[,] maze = FindObjectOfType<MazeGenerator>().GetGrid();
        // Getting path to end point
        Vector2Int start = new Vector2Int(1, 1); // Player's coordinates
        Vector2Int target = new Vector2Int(10, 10); // Exit coordinates
        path = Pathfinding.Instance.FindPath(start, target, maze); // Finding path
        // Starting movement
        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        foreach (Node node in path)
        {
            Vector3 targetPosition = new Vector3(node.position.x, transform.position.y, node.position.y);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
                yield return null;
            }
        }
    }
}
