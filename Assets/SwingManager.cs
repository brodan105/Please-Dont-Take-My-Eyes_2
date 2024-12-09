using System.Collections;
using UnityEngine;

public class SwingManager : MonoBehaviour
{
    public static SwingManager instance;

    float up = 0;
    float down = -180;
    float right = -90;
    float left = 90;

    public bool canSwing = true;

    public Animator slashAnim;
    public GameObject slashHitBox;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (!canSwing) return;

        // Reset attack cube to default (whatever way the player is facing)
        if (PlayerMovement.instance.vertical == 0 && PlayerMovement.instance.horizontal == 0)
        {
            if(PlayerMovement.instance.GetComponent<Transform>().localScale.x == -1)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, left);
            }
            else if(PlayerMovement.instance.GetComponent<Transform>().localScale.x == 1)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, right);
            }
            return;
        }

        const float inputThreshold = 0.1f; // To account for floating-point imprecision

        if (Mathf.Abs(PlayerMovement.instance.horizontal) > Mathf.Abs(PlayerMovement.instance.vertical))
        {
            // Prioritize horizontal movement
            if (PlayerMovement.instance.horizontal > inputThreshold)
            {
                // Moving right
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, right);
            }
            else if (PlayerMovement.instance.horizontal < -inputThreshold)
            {
                // Moving left
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, left);
            }

            slashHitBox.transform.localScale = new Vector3(1.980172f, slashHitBox.transform.localScale.y, slashHitBox.transform.localScale.z);
        }
        else
        {
            // Prioritize vertical movement
            if (PlayerMovement.instance.vertical > inputThreshold)
            {
                // Moving up
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, up);
            }
            else if (PlayerMovement.instance.vertical < -inputThreshold)
            {
                // Moving down
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, down);
            }

            slashHitBox.transform.localScale = new Vector3(0.9646875f, slashHitBox.transform.localScale.y, slashHitBox.transform.localScale.z);
        }
    }
}
