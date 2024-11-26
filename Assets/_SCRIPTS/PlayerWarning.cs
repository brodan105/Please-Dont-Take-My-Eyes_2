using UnityEngine;

public class PlayerWarning : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RumbleManager.instance.RumblePulse(0.25f, 1f, 0.25f);
        }
    }
}
