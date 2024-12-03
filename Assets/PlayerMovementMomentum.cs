using UnityEngine;

public class CharacterControllerWithMomentum : MonoBehaviour
{
    public float speed = 5f; // Maximum speed
    public float acceleration = 10f; // How quickly the character reaches full speed
    public float deceleration = 8f; // How quickly the character slows down
    public Rigidbody2D rb;

    private float currentVelocityX = 0f;

    void Update()
    {
        // Get the horizontal input from the player
        float horizontal = Input.GetAxis("Horizontal");

        // Calculate the target velocity based on input
        float targetVelocityX = horizontal * speed;

        // Adjust the current velocity towards the target, accounting for acceleration and deceleration
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            // Player is providing input, so accelerate
            currentVelocityX = Mathf.MoveTowards(currentVelocityX, targetVelocityX, acceleration * Time.deltaTime);
        }
        else
        {
            // No input, so decelerate
            currentVelocityX = Mathf.MoveTowards(currentVelocityX, 0f, deceleration * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Update the Rigidbody velocity
        rb.linearVelocity = new Vector2(currentVelocityX, rb.linearVelocity.y);
    }
}
