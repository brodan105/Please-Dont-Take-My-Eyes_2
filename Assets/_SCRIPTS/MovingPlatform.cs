using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && PlayerMovement.instance.IsGroundedPlatform())
        {
            collision.transform.parent = transform;
        }
    }

    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player") return;

        if (PlayerMovement.instance.horizontal > 0.25f || !PlayerMovement.instance.IsGrounded())
        {
            collision.transform.parent = null;
        }
        else
        {
            if (PlayerMovement.instance.IsGroundedPlatform())
            {
                collision.transform.parent = transform;
                Debug.Log("You're on top of the platform");
            }
        }
    }
    */


    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
