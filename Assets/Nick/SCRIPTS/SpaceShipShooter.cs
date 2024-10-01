using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingCooldown = 2f;
    public float bulletSpeed = 10f;
    private Transform player;
    private bool isPlayerInRange = false;
    private SphereCollider sphereCollider;

    [SerializeField] private float minX = -100f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float minY = -100f;
    [SerializeField] private float maxY = 100f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(player);

        sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider == null)
        {
            Debug.LogError("SphereCollider component not found! Make sure it is attached to the enemy.");
        }

        StartCoroutine(SpawnBullet());
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            transform.LookAt(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    IEnumerator SpawnBullet()
    {
        while (true)
        {
            if (isPlayerInRange)
            {
                GameObject player = GameObject.FindWithTag("Player");

                Vector3 targetPosition = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z);

                GameObject bulletInstance = Instantiate(bulletPrefab, targetPosition, Quaternion.identity);

                Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();

                StartCoroutine(MoveBullet(bulletRb, player.transform.position));
            }
            yield return new WaitForSeconds(shootingCooldown);
        }
    }

    IEnumerator MoveBullet(Rigidbody bulletRb, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - bulletRb.position).normalized;

        while (bulletRb != null)
        {
            Vector3 newPosition = bulletRb.position + direction * bulletSpeed * Time.fixedDeltaTime;
            bulletRb.MovePosition(newPosition);

            if (IsOutsideWorldLimits(bulletRb.position))
            {
                Destroy(bulletRb.gameObject);
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
}
