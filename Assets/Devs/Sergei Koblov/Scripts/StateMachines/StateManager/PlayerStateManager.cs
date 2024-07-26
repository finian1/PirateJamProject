using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    MOVING,
    JUMPING,
    CROUCHING,
    CROUCHMOVING,
    DASHING,
    LIGHTATTACKING,
}

public class PlayerStateManager : MonoBehaviour, IDamageableObject
{
    //--------------VARIABLES-----------------

    [Header("Movement variables")]
    public float currentMovementSpeed;
    public float originalMovementSpeed;
    public float crouchingMovementSpeed;
    public float horizontalMovement;

    [Header("Attacking variables")]
    public float lightAttackCooldown;
    //public bool justLightAttacked;

    [Header("Jumping variables")]
    public float velocityY;
    public float velocityX;
    public float jumpingPower;
    public float originalJumpingPower;
    public int currentJumpCount;
    public int maxJumpCount;

    public float coyoteTime;
    public float coyoteTimeCounter;
    public float jumpBufferTime;
    public float jumpBufferCounter;

    [Header("Crouching variables")]
    public float crouchFallingTime;
    public float crouchRisingTime;
    public bool isCrouchFalling;
    public bool isCrouchRising;
    public bool isCrouchMoving;


    [Header("Dashing variables")]
    public int initialDashCounter;
    public int currentDashCounter;
    public int minDashCounter;
    public int maxDashCounter;
    public float dashPower;
    public float dashResetTimer;
    public float originalDashCooldownTimer;
    public float dashCooldownTimer;

    [Header("Other variables")]
    public float groundDistance;
    public float groundedTimer;


    [Header("Vectors")]
    public Vector2 moveDirection;
    public Vector3 currentScale;
    public Vector3 originalScale;
    public Vector3 crouchScale;
    public Vector3 mousePosition;


    [Header("Booleans")]
    public bool isGrounded;
    public bool startGroundedTimer;
    public bool canDoubleJump;
    public bool isJumpBuffering;
    public bool isFacingRight;
    public bool isCrouching;
    public bool hasCrouchFlipReset;
    public bool justDashed;


    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerDoubleJumpParticle _playerDoubleJumpParticle;
    public PlayerDashParticle _playerDashParticle;

    [Header("GameObjects")]
    public Camera mainCamera;
    public GameObject groundCheck;
    public BaseWeapon weapon;
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;


    [Header("Layers")]
    public LayerMask groundLayer;

    [Header("Player Stats")]
    public float initialHealth = 100.0f;
    public float currentHealth = 100.0f;

    private float redFlash = 0.0f;


    // ---------------STATES-------------------

    
    PlayerBaseState currentState;

    public Dictionary<PlayerState, PlayerBaseState> PlayerStates = new Dictionary<PlayerState, PlayerBaseState>()
    {
        {PlayerState.IDLE, new PlayerIdleState()},
        {PlayerState.MOVING, new PlayerMovingState()},
        {PlayerState.JUMPING, new PlayerJumpingState()},
        {PlayerState.CROUCHING, new PlayerCrouchingState()},
        {PlayerState.CROUCHMOVING, new PlayerCrouchMovingState()},
        {PlayerState.DASHING, new PlayerDashingState()},
        {PlayerState.LIGHTATTACKING, new PlayerLightAttackingState()},
    };


    void Start()
    {
        currentState = PlayerStates[PlayerState.IDLE];

        currentState.EnterState(this);


        //Movespeed
        originalMovementSpeed = 12f;
        crouchingMovementSpeed = originalMovementSpeed * 0.2f;

        //Attacking
        //justLightAttacked = false;

        //Dashing
        currentDashCounter = 3;
        minDashCounter = 0;
        maxDashCounter = 3;
        dashResetTimer = 0f;
        originalDashCooldownTimer = 0.1f;
        justDashed = false;

        //Jumping
        originalJumpingPower = 25f;
        currentJumpCount = 0;
        maxJumpCount = 2;
        coyoteTime = 0.2f;
        jumpBufferTime = 0.2f;
        isJumpBuffering = false;

        //Crouching
        hasCrouchFlipReset = true;
        isCrouching = false;
        isCrouchRising = false;

        //Other
        isFacingRight = true;
        groundDistance = 0.02f;

        originalScale = transform.localScale;
        currentScale = transform.localScale;
        //crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        crouchScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);

    }

    void Update()
    {
        currentState.UpdateState(this);

        if (startGroundedTimer)
        {
            isGrounded = false;

            groundedTimer += Time.deltaTime;

            if (groundedTimer > 0.1f)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundDistance, groundLayer);
                groundedTimer = 0f;
                startGroundedTimer = false;
            }
        }

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (!startGroundedTimer)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundDistance, groundLayer);
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }

        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
            isJumpBuffering = true;
        }

        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter < 0f || Input.GetKeyDown(KeyCode.LeftShift))
        {
            isJumpBuffering = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && moveDirection.x == 0 && isGrounded)
        {
            crouchRisingTime = 0f;

            isCrouchMoving = false;

            anim.SetBool("IsCrouchIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCrouchMoving", false);
            anim.SetBool("hasCrouchFallingFinished", false);
            anim.SetBool("IsCrouchFalling", true);
            crouchFallingTime += Time.deltaTime;
            isCrouchFalling = true;

            if (crouchFallingTime > 0.1f)
            {
                anim.SetBool("hasCrouchFallingFinished", true);
                anim.SetBool("IsCrouchFalling", false);
                isCrouchFalling = false;
            }
        }

        else if (Input.GetKey(KeyCode.LeftControl) && moveDirection.x != 0 && isGrounded)
        {
            anim.SetBool("IsCrouchIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCrouchMoving", true);
            isCrouchMoving = true;
        }

        //else
        //{
        //    anim.SetBool("IsCrouchIdle", false);
        //    anim.SetBool("IsCrouchMoving", false);
        //    anim.SetBool("IsCrouchMoving", false);
        //    anim.SetBool("hasCrouchFallingFinished", false);
        //    anim.SetBool("IsCrouchFalling", false);
        //}

        if (!Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            crouchFallingTime = 0f;

            anim.SetBool("IsCrouchIdle", false);
            anim.SetBool("hasCrouchRisingFinished", false);
            anim.SetBool("IsCrouchRising", true);
            crouchRisingTime += Time.deltaTime;
            isCrouchRising = true;

            if (crouchRisingTime > 0.1f)
            {
                anim.SetBool("hasCrouchRisingFinished", true);
                anim.SetBool("IsCrouchRising", false);
                isCrouchRising = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            anim.SetBool("IsCrouchHeld", true);
        }

        else
        {
            anim.SetBool("IsCrouchHeld", false);
        }

        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;


        transform.localScale = currentScale;

        currentDashCounter = Mathf.Clamp(currentDashCounter, minDashCounter, maxDashCounter);

        UpdateDashUI();

        DashReset();

        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f - redFlash, 1.0f - redFlash);
        redFlash -= Time.deltaTime;
        redFlash = Mathf.Clamp(redFlash, 0.0f, 1.0f);

    }

    private void FixedUpdate()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void SwitchState(PlayerState state)
    {
        currentState = PlayerStates[state];
        PlayerStates[state].EnterState(this);
    }

    public void DashReset()
    {
        dashCooldownTimer += Time.deltaTime;

        if (currentDashCounter != maxDashCounter)
        {
            dashResetTimer += Time.deltaTime;

            if (dashResetTimer >= 1f)
            {
                currentDashCounter++;
                dashResetTimer = 0f;
            }
        }
    }

    void UpdateDashUI()
    {
        if (dash1 == null || dash2 == null || dash3 == null)
        {
            return;
        }

        GameObject[] dashes = { dash1, dash2, dash3 };

        for (int i = 0; i < dashes.Length; i++)
        {
            dashes[i].SetActive(i < currentDashCounter);
        }
    }

    public void OnDrawGizmos()
    {
        //Groundcheck gameobject visual circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }

    public void Damage(float amount, GameObject source)
    {
        redFlash = 1.0f;
        currentHealth -= amount;
    }

    public void Attack(int index)
    {
        weapon.Attack(index);
    }
}
