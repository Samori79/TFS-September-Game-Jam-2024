using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlancingKnockBack : MonoBehaviour
{
    public float bounceFactor = 0.8f;  // Controls how much momentum is preserved after collision

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Get the player's current velocity
            Vector3 currentVelocity = rb.velocity;

            // Get the normal of the collision (the direction perpendicular to the asteroid's surface)
            Vector3 collisionNormal = collision.contacts[0].normal;

            // Reflect the player's velocity off the asteroid's surface
            Vector3 reflectedVelocity = Vector3.Reflect(currentVelocity, collisionNormal);

            // Apply the reflected velocity, scaled by bounceFactor
            rb.velocity = reflectedVelocity * bounceFactor;
        }
    }
}
