using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubesPerAxis = 3;
    public float explosionForce = 300f;
    public float explosionRadius = 1f;
    public float cubeSize = 0.1f;
    public float cubeLifetime = 2f;

    // Method for activation of explosion
    public void Explode()
    {
        // Getting player's size
        Vector3 playerSize = transform.localScale;
        Vector3 cubeScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // Creating particles on X, Y and Z axes
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCubePiece(x, y, z, cubeScale, playerSize);
                }
            }
        }
    }

    // Method for particle creation
    void CreateCubePiece(int x, int y, int z, Vector3 cubeScale, Vector3 playerSize)
    {
        // Calculating particle position based on player position
        Vector3 piecePosition = transform.position + new Vector3(
            (x - (cubesPerAxis - 1) * 0.5f) * cubeSize,
            (y - (cubesPerAxis - 1) * 0.5f) * cubeSize,
            (z - (cubesPerAxis - 1) * 0.5f) * cubeSize);

        // Creating particle
        GameObject cubePiece = Instantiate(cubePrefab, piecePosition, Quaternion.identity);
        cubePiece.transform.localScale = cubeScale;

        // Adding physics
        Rigidbody rb = cubePiece.GetComponent<Rigidbody>();
        Vector3 explosionDir = cubePiece.transform.position - transform.position;
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

        // Deleting particle after certain amount of time
        Destroy(cubePiece, cubeLifetime);
    }
}