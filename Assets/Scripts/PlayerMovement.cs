using UnityEngine;

public class PlayerDriftMovement : MonoBehaviour
{
    public float thrust = 10f;  // Force applied when moving forward
    public float rotationSpeed = 100f;  // Speed of rotation (turning)
    public float drag = 0.99f;  // Drag for drifting effect
    public float decelerationForce = 5f;  // Reverse force applied when pressing 'S'
    public float maxSpeed = 20f;  // Maximum allowed speed for the player
    private bool isStunned = false; //stun state, default false
    private float stunTimer = 0f; //time remaining on stun



    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;  // Disable gravity
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;  // Lock to X-Y plane
    }

    void Update()
    {

        //stun logic. If player is stunned, ignore all other input
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false; //end stun after the timer runs out.
            }
            return; //ignores input during the stun.
        }

        // Rotation: A/D or Left/Right arrow keys rotate the player around the Z-axis
        float turn = Input.GetAxis("Horizontal");  // "Horizontal" maps to A/D or Left/Right
        transform.Rotate(0, 0, -turn * rotationSpeed * Time.deltaTime);

        // Forward movement: W key moves the player in the direction it's facing
        if (Input.GetKey(KeyCode.W))  // "W" key for forward movement
        {
            // Move the player in the direction it is facing (along the X-Y plane)
            rb.AddForce(transform.up * thrust);  // Use transform.up for forward direction in a 2D plane
            Debug.Log("Applying forward force: " + transform.up * thrust);

           
        }

        if (Input.GetKey(KeyCode.S))  // "S" key for deceleration
        {
            rb.AddForce(-transform.up * decelerationForce);  // Apply force in the opposite direction of movement
            Debug.Log("Applying reverse force: " + -transform.up * decelerationForce);
        }
    }

    void FixedUpdate()
    {
        // Apply drag to the velocity to simulate drifting effect
        rb.velocity *= drag;
        Debug.Log("Current Velocity: " + rb.velocity);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            Debug.Log("Capping speed: " + rb.velocity);
        }

        Debug.Log("Current Velocity: " + rb.velocity);

    }

    public void StunPlayer(float duration)
    {
        isStunned = true;
        stunTimer = duration;
       
        //uncomment this if we want to get rid of all movement
        //rb.velocity = Vector3.zero;
    }
}