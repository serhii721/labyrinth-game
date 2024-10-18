using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZoneGenerator : MonoBehaviour
{
    public GameObject hazardPrefab;
    public int numberOfHazards;

    private List<GameObject> zones = new List<GameObject>();

    public void GenerateHazardZones(bool[,] mazeGrid)
    {
        ClearOldZones();
        for (int i = 0; i < numberOfHazards; ++i)
        {
            // Regenerate position until there's no wall
            int randomX;
            int randomZ;
            do
            {
                randomX = Random.Range(1, mazeGrid.GetLength(1) - 1);
                randomZ = Random.Range(1, mazeGrid.GetLength(0) - 1);
            } while (!IsAvailablePosition(mazeGrid, randomX, randomZ)); 

            // Create hazard zone
            Vector3 position = new Vector3(randomX, 0.05f, randomZ); // Y = 0.05 to avoid collision with the floor
            GameObject zone = Instantiate(hazardPrefab, position, Quaternion.identity);
            zones.Add(zone);
        }
        Debug.Log("Hazard zones generated");
    }

    private bool IsAvailablePosition(bool[,] mazeGrid, int x, int z)
    {
        // Empty space and not starting and ending points
        if (mazeGrid[x, z]) return false;

        if (x == 1 && z == 1) return false;

        if (x == 10 && z == 10) return false;

        return true;
    }

    private void ClearOldZones()
    {
        // Deleting zones that were created before
        foreach (GameObject zone in zones)
            Destroy(zone);

        zones.Clear();
    }
}