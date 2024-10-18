using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public int width = 12;
    public int height = 12;
    private bool[,] grid;

    private List<GameObject> walls = new List<GameObject>();

    void Start()
    {
        GenerateMaze();
    }

    public void GenerateMaze()
    {
        ClearOldMaze();

        grid = new bool[width, height];

        // Regenerating maze until there is a clear path to finish
        Vector2Int start = new Vector2Int(1, 1); // Player's coordinates
        Vector2Int target = new Vector2Int(10, 10); // Exit coordinates
        do
        {
            // Random walls generation
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    if (x == 0 || x == width - 1 || z == 0 || z == height - 1) // Walls are always on edges
                    {
                        grid[x, z] = true; // Place wall
                    }
                    else if ((x == 1 && z == 1) || (x == 10 & z == 10)) // Start and end points are always empty
                    {
                        grid[x, z] = false; // Leave empty space
                    }
                    else
                    {
                        // Filling internal blocks with walls with 30% chance
                        grid[x, z] = (Random.value < 0.3f) ? true : false;
                    }
                }
            }
        } while (Pathfinding.Instance.FindPath(start, target, grid) == null);
        // After grid generation - building visual walls
        BuildMaze();
        Debug.Log("Maze generated");
        // Generating hazard zones
        FindObjectOfType<HazardZoneGenerator>().GenerateHazardZones(grid);
    }

    void BuildMaze()
    {
        // Spawning walls based on grid
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (grid[x, z] == true) // If cell contains wall
                {
                    // Calculate wall position
                    Vector3 position = new Vector3(x, 0.5f, z);
                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity); // Spawn wall prefab
                    walls.Add(wall);
                }
            }
        }
    }

    void ClearOldMaze()
    {
        // Deleting walls that were created before
        foreach (GameObject wall in walls)
            Destroy(wall);

        walls.Clear();
    }

    public bool[,] GetGrid()
    {
        return grid;
    }
}
