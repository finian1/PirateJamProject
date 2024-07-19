using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    MOVING,
    JUMPING,
    CROUCHING,
    CROUCHMOVING,
}

public class PlayerStateManager : MonoBehaviour
{
    //--------------VARIABLES-----------------

    [Header("Player variables")]
    public float currentMovementSpeed;
    public float originalMovementSpeed;
    public float crouchingMovementSpeed;
    public float horizontalMovement;

    public int jumpCount;
    public float jumpingPower;

    public float groundDistance;


    [Header("Vectors")]
    public Vector3 currentScale;
    public Vector3 originalScale;
    public Vector3 crouchScale;


    [Header("Booleans")]
    public bool isGrounded;
    public bool isFacingRight;
    public bool isCrouching;
    public bool hasCrouchFlipReset;


    [Header("Components")]
    public Rigidbody2D rb;


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
    };

    void Start()
    {
        currentState = PlayerStates[PlayerState.IDLE];

        currentState.EnterState(this);

        originalMovementSpeed = 8f;
        crouchingMovementSpeed = originalMovementSpeed * 0.5f;

        jumpCount = 1;
        groundDistance = 0.4f;

        isFacingRight = true;
        isCrouching = false;
        hasCrouchFlipReset = true;

        originalScale = transform.localScale;
        currentScale = transform.localScale;
        crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);

    }

    void Update()
    {
        currentState.UpdateState(this);

        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundDistance, groundLayer);

        transform.localScale = currentScale;
    }

    public void SwitchState(PlayerState state)
    {
        currentState = PlayerStates[state];
        PlayerStates[state].EnterState(this);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
}
