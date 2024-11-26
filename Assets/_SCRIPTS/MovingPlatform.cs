using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player") return;

        if (PlayerMovement.instance.horizontal != 0)
        {
            collision.transform.parent = null;
        }
        else
        {
            if (collision.transform.position.y > transform.position.y)
            {
                collision.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
