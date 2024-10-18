using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3[] path;
    private int targetIndex;

    void Start()
    {
        Invoke("StartMoving", 2f); // 2 seconds delay before movement
    }

    void StartMoving()
    {
        // TODO
        // Getting path to end point
        //path = Pathfinding.Instance.FindPath(transform.position, new Vector3(10, 0, 10)); // Example: end point
        targetIndex = 0;
        StartCoroutine(MoveAlongPath());
    }

    IEnumerator MoveAlongPath()
    {
        while (targetIndex < path.Length)
        {
            Vector3 targetPos = path[targetIndex];
            while (transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            targetIndex++;
        }
    }
}
