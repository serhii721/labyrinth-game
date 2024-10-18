using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZoneGenerator : MonoBehaviour
{
    public GameObject hazardPrefab;
    public int numberOfHazards;
    public float minX, maxX, minZ, maxZ; // Restrictions for zone generation

    void Start()
    {
        GenerateHazardZones(FindObjectOfType<MazeGenerator>().GetGrid()); // Generating zone placement based on current labyrinth
    }

    void GenerateHazardZones(bool[,] mazeGrid)
    {
        for (int i = 0; i < numberOfHazards; ++i)
        {
            // Regenerate position until there's no wall
            int randomX;
            int randomZ;
            do
            {
                randomX = Random.Range(1, mazeGrid.GetLength(1));
                randomZ = Random.Range(1, mazeGrid.GetLength(0));
            } while (mazeGrid[randomX, randomZ]);

            // Create hazard zone
            Vector3 position = new Vector3(randomX, 0.05f, randomZ); // Y = 0.05 to avoid collision with the floor
            Instantiate(hazardPrefab, position, Quaternion.identity);
        }
    }
}