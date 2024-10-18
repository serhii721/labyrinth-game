using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public int width = 10;
    public int height = 10;
    private int[,] grid;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        grid = new int[width, height];

        // Random walls generation
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Walls are always on edges
                if (x == 0 || x == width - 1 || z == 0 || z == height - 1)
                {
                    grid[x, z] = 1; // Place wall
                }
                else
                {
                    // Filling internal blocks with walls with 30% chance
                    grid[x, z] = (Random.value < 0.3f) ? 1 : 0;
                }
            }
        }

        // After grid generation - building visual walls
        BuildMaze();
    }

    void BuildMaze()
    {
        // Spawning walls based on grid
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (grid[x, z] == 1) // If cell contains wall
                {
                    // Calculate wall position
                    Vector3 position = new Vector3(x, 0.5f, z);
                    Instantiate(wallPrefab, position, Quaternion.identity); // Spawn wall prefab
                }
            }
        }
    }
}
