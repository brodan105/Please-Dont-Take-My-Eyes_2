using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    [SerializeField] Transform leftPosition;
    [SerializeField] Transform rightPosition;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] bool verticalMovement = false;

    Rigidbody2D rb;

    Transform currentPoint;

    // State SETTER
    private enum State
    {
        Roaming,
        ChargePlayer,
        LostPlayer,
    }
    
    // State reference
    private State state;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set default state to 'Roaming'
        state = State.Roaming;

        currentPoint = leftPosition;
    }

    private void Update()
    {
        // Acts like a switch between the different "states".
        // Swapping between instead of having multiple different IF statements.
        switch (state)
        {
            default:
            case State.Roaming:
                // Roaming code logic goes here
                RoamingPositions();
                // Looking for target
                //FindTarget();
                break;

            case State.ChargePlayer:
                // Chase player logic goes here
                break;

            case State.LostPlayer:
                // Enemy finished attack and is looking to reaquire the player with higher parameters,
                // if player can be found then attack again, if not then change back to roaming state and return to last position
                break;
        }
    }

    private void RoamingPositions()
    {
        // Enemy roaming logic
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == leftPosition)
        {
            if (!verticalMovement)
            {
                // Move Left
                rb.linearVelocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                // Move Up
                rb.linearVelocity = new Vector2(0, moveSpeed);
            }
        }
        else
        {
            if (!verticalMovement)
            {
                // Move Right
                rb.linearVelocity = new Vector2(-moveSpeed, 0);
            }
            else
            {
                // Move Down
                rb.linearVelocity = new Vector2(0, -moveSpeed);
            }
        }

        

        if(Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == rightPosition.transform)
        {
            Flip();
            currentPoint = leftPosition;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == leftPosition.transform)
        {
            Flip();
            currentPoint = rightPosition;
        }
    }

    private void ChargePlayer()
    {
        // Roll animation

        // Stop enemy movement

        // Increase speed

        StartCoroutine(WindUpAttack());
    }

    // Checks the distance between the character and this, then changes the state
    private void FindTarget()
    {
        // Find player logic here
        if(Vector2.Distance(transform.position, PlayerMovement.instance.transform.position) < 5)
        {
            state = State.ChargePlayer;
        }
        else
        {
            state = State.Roaming;
        }
    }

    // Flips the local scale of the monster
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (leftPosition == null || rightPosition == null) return;

        Gizmos.DrawWireSphere(leftPosition.position, 1f);
        Gizmos.DrawWireSphere(rightPosition.position, 1f);
        Gizmos.DrawLine(rightPosition.position, leftPosition.position);
    }

    private IEnumerator WindUpAttack()
    {
        yield return new WaitForSeconds(1f);

        // Set enemy attack direction in direction of Player's X position relative to the enemy

        // Move enemy in direction at new speed for 3 seconds, if the bug comes into contact with a object play bumped animation and turn
        // the bug into a hitable enemy (the player can defeat now), or if not, return them to idle animation and search for player (change states)
    }
}
