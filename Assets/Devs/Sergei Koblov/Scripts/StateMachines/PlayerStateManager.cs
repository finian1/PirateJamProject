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
}

public class PlayerStateManager : MonoBehaviour
{
    //--------------VARIABLES-----------------

    [Header("Movement variables")]
    public float currentMovementSpeed;
    public float originalMovementSpeed;
    public float crouchingMovementSpeed;
    public float horizontalMovement;

    [Header("Jumping variables")]
    public float velocityY;
    public float velocityX;
    public float jumpingPower;
    public float originalJumpingPower;
    public int currentJumpCount;
    public int maxJumpCount;

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


    [Header("Booleans")]
    public bool isGrounded;
    public bool startGroundedTimer;
    public bool canDoubleJump;
    public bool isFacingRight;
    public bool isCrouching;
    public bool hasCrouchFlipReset;
    public bool justDashed;


    [Header("Components")]
    public Rigidbody2D rb;


    [Header("GameObjects")]
    public GameObject groundCheck;
    public BaseWeapon weapon;
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;


    [Header("Layers")]
    public LayerMask groundLayer;


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
    };


    void Start()
    {
        currentState = PlayerStates[PlayerState.IDLE];

        currentState.EnterState(this);


        //Movespeed
        originalMovementSpeed = 12f;
        crouchingMovementSpeed = originalMovementSpeed * 0.5f;

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

        //Crouching
        hasCrouchFlipReset = true;
        isCrouching = false;

        //Other
        isFacingRight = true;
        groundDistance = 0.02f;

        originalScale = transform.localScale;
        currentScale = transform.localScale;
        crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);

    }

    void Update()
    {
        currentState.UpdateState(this);

        if(startGroundedTimer)
        {
            isGrounded = false;

            groundedTimer += Time.deltaTime;

            if(groundedTimer > 0.1f)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundDistance, groundLayer);
                groundedTimer = 0f;
                startGroundedTimer = false;
            }
        }

        if(!startGroundedTimer)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundDistance, groundLayer);
        }


        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;


        transform.localScale = currentScale;


        //moveDirection = _playerInputSystem.move.action.ReadValue<Vector2>();
        //moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        currentDashCounter = Mathf.Clamp(currentDashCounter, minDashCounter, maxDashCounter);

        if(currentDashCounter == 3)
        {
            dash1.SetActive(true);
            dash2.SetActive(true);
            dash3.SetActive(true);
        }

        if (currentDashCounter == 2)
        {
            dash1.SetActive(true);
            dash2.SetActive(true);
            dash3.SetActive(false);
        }

        if (currentDashCounter == 1)
        {
            dash1.SetActive(true);
            dash2.SetActive(false);
            dash3.SetActive(false);
        }

        if (currentDashCounter == 0)
        {
            dash1.SetActive(false);
            dash2.SetActive(false);
            dash3.SetActive(false);
        }

        DashReset();

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

    public void OnDrawGizmos()
    {
        //Groundcheck gameobject visual circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
}
