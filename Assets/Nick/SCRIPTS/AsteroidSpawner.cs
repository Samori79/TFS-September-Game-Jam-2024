using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 5;
    public float spawnAreaSize = 10f;
    public float minDistance = 1f;
    public float minDistanceFromPlayer = 2f;

    private List<Vector3> asteroidPositions = new List<Vector3>();

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position;

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector3 newPosition;
            bool positionValid;

            do
            {
                newPosition = new Vector3(
                    Random.Range(-spawnAreaSize, spawnAreaSize),
                    Random.Range(-spawnAreaSize, spawnAreaSize),
                    playerPosition.z
                );

                positionValid = IsPositionValid(newPosition, playerPosition);

            } while (!positionValid);

            asteroidPositions.Add(newPosition);
            Instantiate(asteroidPrefab, newPosition, Quaternion.identity);
        }
    }

    bool IsPositionValid(Vector3 position, Vector3 playerPosition)
    {
        if (Vector3.Distance(position, playerPosition) < minDistanceFromPlayer)
        {
            return false;
        }

        foreach (var asteroidPosition in asteroidPositions)
        {
            if (Vector3.Distance(position, asteroidPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
