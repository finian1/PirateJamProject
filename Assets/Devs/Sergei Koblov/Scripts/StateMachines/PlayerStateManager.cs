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

    [Header("Player variables")]
    //Moving
    public float currentMovementSpeed;
    public float originalMovementSpeed;
    public float crouchingMovementSpeed;
    public float horizontalMovement;

    //Jumping
    public float jumpingPower;
    public float originalJumpingPower;
    public int currentJumpCount;
    public int maxJumpCount;

    //Dashing (wow this was really hard for some reason holy moly)
    public int initialDashCounter;
    public int currentDashCounter;
    public int minDashCounter;
    public int maxDashCounter;
    public float dashPower;
    public float dashResetTimer;
    public float originalDashCooldownTimer;
    public float dashCooldownTimer;

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


    //[Header("Input Buttons Booleans")]

    [HideInInspector] public bool jumpPress;
    [HideInInspector] public bool crouchPress;
    [HideInInspector] public bool fire1Press;
    [HideInInspector] public bool fire2Press;
    [HideInInspector] public bool dashPress;


    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerInputSystem _playerInputSystem;


    [Header("GameObjects")]
    public GameObject groundCheck;
    public BaseWeapon weapon;


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

        _playerInputSystem = GetComponent<PlayerInputSystem>();

        //Movespeed
        originalMovementSpeed = 12f;
        crouchingMovementSpeed = originalMovementSpeed * 0.5f;

        //Dashing
        currentDashCounter = 3;
        minDashCounter = 0;
        maxDashCounter = 3;
        dashResetTimer = 0f;
        originalDashCooldownTimer = 0.2f;
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

        groundDistance = 0.01f;

        originalScale = transform.localScale;
        currentScale = transform.localScale;
        crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        animator = GetComponent<Animator>();

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



        transform.localScale = currentScale;

        moveDirection = _playerInputSystem.move.action.ReadValue<Vector2>();


        if (jumpPress)
        {
            jumpPress = false;
        }

        if (crouchPress)
        {
            crouchPress = false;
        }

        if (fire1Press)
        {
            fire1Press = false;
        }

        if (fire2Press)
        {
            fire2Press = false;
        }

        if (dashPress)
        {
            dashPress = false;
        }

        currentDashCounter = Mathf.Clamp(currentDashCounter, minDashCounter, maxDashCounter);

        DashReset();

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
