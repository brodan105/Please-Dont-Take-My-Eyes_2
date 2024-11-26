using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    #region References
    [Header("References")]
    public LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private ParticleSystem runParticle;
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private PlayerInput _input;
    #endregion

    public bool playerFirstMove = true;
    private Vector3 playerStartPos;

    #region Player Properties
    [Header("Player Properties")]
    [SerializeField] [Range(1f, 20f)] private float speed = 8f;
    [SerializeField] [Range(5f, 30f)] private float jumpingPower = 16f;

    // Coyote Time
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Jump Buffer
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    // Wall Slide
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private float wallSlidingSpeed = 2f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    public float horizontal;
    public bool canMove = true;

    private bool canCheckForInput;
    private float vertical;
    private bool isFacingRight = true;
    private bool isJumping;

    bool sfxPlaying;
    #endregion

    private void Awake()
    {
        instance = this;

        // Grabs reference to Rigidbody2D on the parent component
        rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();

        // Set player start position
        playerStartPos = transform.position;

        canMove = false;
        StartCoroutine(CheckForInputDelay());
    }

    private void Update()
    {
        if (canMove && PlayerDie.instance.fadePanel.GetComponent<Image>().color.a < 0.25f)
        {
            // Allows player to move vertically but locks them horizontally
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        }

        // Check if need to start speedrun timer
        if (Vector3.Distance(transform.position, playerStartPos) > 0.2f && playerFirstMove && canCheckForInput && !PlayerDie.instance.hasDied && PlayerDie.instance.fadePanel.GetComponent<Image>().color.a < 0.25f)
        {
            playerFirstMove = false;
            TimeController.instance.StartTimer();
        }

        // Check if player is grounded, if so then start coyote time timer
        #region IsGrounded/Coyote Time
        if (IsGrounded())
        {
            anim.SetBool("isGrounded", true);

            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        #endregion

        // Activate run particle
        #region Run Particle
        var move = _input.actions["Move"];
        Vector2 moveVec = move.ReadValue<Vector2>();
        var em = runParticle.emission;

        // Check if moving and grounded
        if (moveVec.x != 0 && IsGrounded())
        {
            em.enabled = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            em.enabled = false;
            anim.SetBool("isRunning", false);
        }

        if (isWallSliding)
        {
            em.enabled = true;
        }
        #endregion

        // Activate jump and start jump buffer timer, activates wall jump if within parameters and flips player after jump
        #region Jump/Jump Buffer/WallJump
        var up = _input.actions["Jump"];
        if (up.WasPressedThisFrame() && canMove)
        {
            anim.SetTrigger("jump");
            anim.SetBool("isGrounded", false);

            jumpBufferCounter = jumpBufferTime;

            if (wallJumpingCounter > 0f)
            {
                StartCoroutine(WallJumpTimer());
                isWallJumping = true;
                rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = 0f;

                // Jump SFX
                PlayerAudioController.instance.playerJump();

                if (transform.localScale.x != wallJumpingDirection)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }

                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && !isJumping && !isWallSliding)
        {
            // Apply normal jumping velocity
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpingPower);
            jumpBufferCounter = 0;

            // Jump SFX
            PlayerAudioController.instance.playerJump();

            StartCoroutine(JumpCooldown());
        }
        #endregion

        // Check if player needs to flip
        #region Check flip
        // Checks the input of the player (horizontal), checks if the player is already facing right, and determines if it needs to flip.
        if (!isWallJumping)
        {
            if (!canMove) return;

            if (!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }
        }
        #endregion

        WallSlide();
        WallJump();
    }

    #region Player Movement Control
    public void StopMovement()
    {
        canMove = false;
        rb.linearVelocity = new Vector2(0, 0);
    }

    public void StartMovement()
    {
        canMove = true;
    }
    #endregion

    // Check if grounded
    #region IsGrounded
    // A boolean to call ISGrounded
    public bool IsGrounded()
    {
        // Will return IsGrounded = true if the physics2D circle (with a radius of 0.2f at the position of the ground check object) reads anything on the
        // "groundLayer" (the layermask you set the ground to
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    #endregion

    #region Wall Jump/Slide
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        

        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    #endregion

    // This method flips the player's transform to simulate it turning sides
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void ImpulseJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpingPower);
    }

    public void JumpPad(float jPower)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jPower);
    }

    #region Input Actions

    // This function changes the value of "horizontal" to be used by the player's control in the Update() method. Horizontal becomes the variable that changes
    // depending on if the player is using the controls set by the InputAction Asset.
    public void Move(InputAction.CallbackContext context)
    {
        // Has to read the x-axis only, because horizontal is origially a float, a float cannot read a Vector2 because a Vector2 returns two floats
        // (x-axis float, and a y-axis float).
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    // Uses the InputAction.CallbackContext to reference the InputAction event, where it will reference your keybind placed from the Input Action Asset
    // This method checks if the player is pressing or not pressing the jump key, then applies forces accordingly if the player IsGrounded
    public void Jump(InputAction.CallbackContext context)
    {
        // Check if the player pressed jump and IsGrounded
        
        // Check if the player let go of the jump key, midway though jumping
        if (context.canceled && rb.linearVelocityY > 0f)
        {
            // Change the player's vertical velocity (y-axis), and dampen it by 0.5f (half the power of what it had)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);

            // Prevents player from double jumping
            coyoteTimeCounter = 0f;
        }
    }
    #endregion


    #region Ienumerators
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    // Movement control delay after wall jumping
    private IEnumerator WallJumpTimer()
    {
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        canMove = true;
    }

    public IEnumerator CheckForInputDelay()
    {
        yield return new WaitForSeconds(2f);
        StartMovement();
        canCheckForInput = true;
    }
    #endregion
}
