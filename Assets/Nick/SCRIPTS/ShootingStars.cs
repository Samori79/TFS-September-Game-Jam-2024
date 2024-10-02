using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStars : MonoBehaviour
{
    [SerializeField] private int XRange = 50;
    [SerializeField] private int YRange = 50;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float spawnInterval = 2f;
    public GameObject ShootingStar;

    [SerializeField] private float minX = -100f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float minY = -100f;
    [SerializeField] private float maxY = 100f;

    [SerializeField] private float deviationAngle = 15f;

    void Start()
    {
        StartCoroutine(SpawnShootingStars());
    }

    IEnumerator SpawnShootingStars()
    {
        while (true)
        {
            GameObject player = GameObject.FindWithTag("Player");

            // Calculate random position around the player
            int randomX = XRange;
            int randomY = YRange;
            int randomSide = Random.Range(0, 4);

            if (randomSide == 0) // Top side
            {
                randomX = Random.Range(-XRange, XRange);
                randomY = YRange; // Spawn above the player
            }
            if (randomSide == 1) // Bottom side
            {
                randomX = Random.Range(-XRange, XRange);
                randomY = -YRange; // Spawn below the player
            }
            if (randomSide == 2) // Left side
            {
                randomX = -XRange;
                randomY = Random.Range(-YRange, YRange);
            }
            if (randomSide == 3) // Right side
            {
                randomX = XRange;
                randomY = Random.Range(-YRange, YRange);
            }

           // Debug.Log(randomSide);
           // Debug.Log(randomX);
           // Debug.Log(randomY);

            Vector3 randomPosition = new Vector3(player.transform.position.x + randomX, player.transform.position.y + randomY, player.transform.position.z);

            GameObject starInstance = Instantiate(ShootingStar, randomPosition, Quaternion.identity);

            Rigidbody starRb = starInstance.GetComponent<Rigidbody>();

            StartCoroutine(MoveShootingStar(starRb, player.transform.position));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveShootingStar(Rigidbody starRb, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - starRb.position).normalized;

        float randomAngle = Random.Range(-deviationAngle, deviationAngle);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
        direction = rotation * direction;

        while (starRb != null)
        {
            Vector3 newPosition = starRb.position + direction * speed * Time.fixedDeltaTime;
            starRb.MovePosition(newPosition);

            if (IsOutsideWorldLimits(starRb.position))
            {
                Destroy(starRb.gameObject);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    bool IsOutsideWorldLimits(Vector3 position)
    {
        return position.x < minX || position.x > maxX ||
               position.y < minY || position.y > maxY;
    }

    void Update()
    {
    }
}
