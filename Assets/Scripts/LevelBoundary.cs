using UnityEngine;

public class WrapAroundBoundary : MonoBehaviour
{
    public float boundaryX = 10f;
    public float boundaryY = 10f;

    void OnDrawGizmos()
    {
        // Set the color for the boundary box
        Gizmos.color = Color.green;

        // Draw a wireframe rectangle to represent the boundary in the scene
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boundaryX * 2, boundaryY * 2, 0f));
    }
}
