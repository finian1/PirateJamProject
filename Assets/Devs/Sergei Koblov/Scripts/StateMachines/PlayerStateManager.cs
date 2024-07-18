using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

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


    [Header("Layers")]
    public LayerMask groundLayer;


    // ---------------STATES-------------------

    PlayerBaseState currentState;

    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerMovingState MovingState = new PlayerMovingState();
    public PlayerJumpingState JumpingState = new PlayerJumpingState();
    public PlayerCrouchingState CrouchingState = new PlayerCrouchingState();
    public PlayerCrouchMovingState CrouchMovingState = new PlayerCrouchMovingState();



    void Start()
    {
        currentState = IdleState;

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

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
}
