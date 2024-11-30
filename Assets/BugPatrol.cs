using System.Collections;
using UnityEngine;

public class BugPatrol : MonoBehaviour
{
    [Header("True/False")]
    [SerializeField] bool startPatrol_Right;

    Rigidbody2D rb;
    [Header("References")]
    [SerializeField] KillBug kBug;

    [Header("Animators")]
    [SerializeField] Animator bugAnim_Main;
    [SerializeField] Animator bugAnim_Body;

    [Header("Destinations")]
    [SerializeField] Transform destinationLeft, destinationRight;
    Transform currentDestination;
    bool destinationIsLeft;

    [Header("Properties")]
    [SerializeField] float transitionDuration = 1f;
    [SerializeField] float speed = 2f;
    [SerializeField] float turnPause;

    // DEBUG
    float DistanceToTarget;

    bool isPaused = false;
    bool atTarget;

    private void Awake()
    {
        bugAnim_Main = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (startPatrol_Right)
        {
            bugAnim_Main.SetBool("isFacingRight", true);
            currentDestination = destinationRight;
            destinationIsLeft = false;
        }
        else
        {
            currentDestination = destinationLeft;
            destinationIsLeft = true;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, currentDestination.position);
        DistanceToTarget = distance;

        if (destinationIsLeft)
        {
            currentDestination = destinationLeft;
        }
        else
        {
            currentDestination = destinationRight;
        }

        if(distance <= 0.5f && !atTarget)
        {
            atTarget = true;
            SwapDestination();
        }

        if (atTarget && !isPaused)
        {
            isPaused = true;
            StartCoroutine(patrolPause());
        }

        if(!isPaused)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, speed * Time.deltaTime);
            bugAnim_Body.SetBool("isWalking", true);
        }
        else
        {
            bugAnim_Body.SetBool("isWalking", false);
        }
    }

    void Turn()
    {
        if (!kBug.isDead)
        {
            bugAnim_Main.SetTrigger("Turn");
        }
    }

    void SwapDestination()
    {
        destinationIsLeft = !destinationIsLeft;
    }

    IEnumerator patrolPause()
    {
        yield return new WaitForSeconds(turnPause);
        isPaused = false;
        atTarget = false;
        Turn();
    }
}
