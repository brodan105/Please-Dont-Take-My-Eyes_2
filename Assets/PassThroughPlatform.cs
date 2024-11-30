using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassThroughPlatform : MonoBehaviour
{
    bool playerOnPlatform;
    bool colliderDisabled;

    Collider2D platformCollider;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(playerOnPlatform && PlayerMovement.instance.vertical < -0.25f)
        {
            if (!colliderDisabled)
            {
                StartCoroutine(DisableCollider());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerOnPlatform = false;
        }
    }

    IEnumerator DisableCollider()
    {
        // Disable collider of platform
        colliderDisabled = true;
        platformCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        // re-enable collider
        colliderDisabled = false;
        platformCollider.enabled = true;
    }
}
