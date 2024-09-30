using UnityEngine;
public class PlayerWrapAround : MonoBehaviour
{
    public float minX, maxX, minY, maxY;

    void Update()
    {
        Vector3 position = transform.position;

        // Check if player crosses the right boundary
        if (position.x > maxX)
            position.x = minX;
        // Check if player crosses the left boundary
        else if (position.x < minX)
            position.x = maxX;

        // Check if player crosses the top boundary
        if (position.y > maxY)
            position.y = minY;
        // Check if player crosses the bottom boundary
        else if (position.y < minY)
            position.y = maxY;

        transform.position = position;
    }
}