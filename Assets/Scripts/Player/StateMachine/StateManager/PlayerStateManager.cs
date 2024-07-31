using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public enum PlayerState
{
    IDLE,
    MOVING,
    JUMPING,
    CROUCHING,
    CROUCHMOVING,
    DASHING,
    LIGHTATTACKING,
    LIGHTSHADOWATTACKING,
    HIDDEN,
}

public class PlayerStateManager : MonoBehaviour, IDamageableObject
{
    //--------------VARIABLES-----------------

    [Header("Movement variables")]
    public float currentMovementSpeed;
    public float originalMovementSpeed = 12.0f;
    public float crouchingSpeedPercent = 0.2f;
    [NonSerialized]
    public float crouchingMovementSpeed;
    [NonSerialized]
    public float horizontalMovement;

    [Header("Attacking variables")]
    public float lightAttackCooldown = 0.25f;
    [NonSerialized]
    public float lightAttackCooldownTimer;
    [NonSerialized]
    public bool justLightAttacked = false;

    [Header("Shadow attacking variables")]
    public float lightShadowAttackCooldown = 0.5f;
    [NonSerialized]
    public float lightShadowAttackCooldownTimer;
    [NonSerialized]
    public bool lightShadowAttackStarted;
    [NonSerialized]
    public bool justLightShadowAttacked;
    public bool canLightShadowAttack = true;

    [Header("Jumping variables")]
    [NonSerialized]
    public float velocityY;
    [NonSerialized]
    public float velocityX;
    [NonSerialized]
    public float jumpingPower;
    public float originalJumpingPower = 25.0f;
    [NonSerialized]
    public int currentJumpCount;
    public int maxJumpCount = 2;

    public float coyoteTime = 0.2f;
    [NonSerialized]
    public float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    [NonSerialized]
    public float jumpBufferCounter;

    [Header("Crouching variables")]
    [NonSerialized]
    public float crouchFallingTime;
    [NonSerialized]
    public float crouchRisingTime;
    public float canCrouchMoveTimer = 0.0f;
    [NonSerialized]
    public bool isCrouchFalling;
    [NonSerialized]
    public bool isCrouchRising;
    [NonSerialized]
    public bool isCrouchMoving;
    [NonSerialized]
    public bool hasPressedCrouch;


    [Header("Dashing variables")]
    public int initialDashCounter;
    public int currentDashCounter = 3;
    public int minDashCounter = 0;
    public int maxDashCounter = 3;
    public float dashPower = 80.0f;
    public float dashResetTime = 1.0f;
    [NonSerialized]
    public float dashResetTimer;
    public float originalDashCooldownTimer = 0.1f;
    [NonSerialized]
    public float dashCooldownTimer;

    [Header("Other variables")]
    //public float ceilingDistance;
    [NonSerialized]
    public float groundedTimer;


    [Header("Vectors")]
    public Vector2 moveDirection;
    public Vector3 currentScale;
    public Vector3 ceilingCubeSize = new Vector3(0.6f, 1.0f, 1.0f);
    public Vector3 originalScale;
    public Vector3 crouchScale;
    public Vector3 mousePosition;


    [Header("Booleans")]
    public bool isGrounded;
    public bool startGroundedTimer;
    public bool canDoubleJump;
    public bool isUnderCeiling;
    public bool isJumpBuffering;
    public bool isFacingRight = true;
    public bool isCrouching;
    public bool canCrouchMove = true;
    public bool hasCrouchFlipReset = true;
    public bool justDashed;
    public bool isHidden;
    public bool unhiding;


    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerDoubleJumpParticle _playerDoubleJumpParticle;
    public PlayerDashParticle _playerDashParticle;

    [Header("Audio")]
    public RandomAudioScript stepAudio;
    public RandomAudioScript attackAudio;

    [Header("GameObjects")]
    public Camera mainCamera;
    public GameObject groundCheck;
    public GameObject ceilingCheck;
    public BaseWeapon weapon;
    public GameObject dash1;
    public GameObject dash2;
    public GameObject dash3;
    public GameObject currentHidingPlace;


    [Header("Layers & Colliders")]
    public LayerMask groundLayer;
    public Collider2D[] hits;

    public int playerLayer;
    public int enemyLayer;


    private float redFlash = 0.0f;

    private RespawnManager respawnManager;


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
        {PlayerState.LIGHTSHADOWATTACKING, new PlayerLightShadowAttackingState()},
        {PlayerState.HIDDEN, new PlayerHiddenState()},
    };


    void Start()
    {
        respawnManager = FindFirstObjectByType<RespawnManager>();
        currentState = PlayerStates[PlayerState.IDLE];

        currentState.EnterState(this);
        //Movespeed
        crouchingMovementSpeed = originalMovementSpeed * crouchingSpeedPercent;

        originalScale = transform.localScale;
        currentScale = transform.localScale;
        //crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        crouchScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);

        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {

        currentState.UpdateState(this);

        //Time.timeScale = 0.333333f;

        //------------------------

        if (startGroundedTimer)
        {
            isGrounded = false;

            groundedTimer += Time.deltaTime;

            if (groundedTimer > 0.1f)
            {
                UpdateIsGrounded();
                
                groundedTimer = 0f;
                startGroundedTimer = false;
            }
        }

        //------------------------

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.localScale = currentScale;

        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;

        currentDashCounter = Mathf.Clamp(currentDashCounter, minDashCounter, maxDashCounter);

        //------------------------

        if(lightShadowAttackStarted)
        {
            canLightShadowAttack = false;
            lightShadowAttackCooldownTimer += Time.deltaTime;

            if(lightShadowAttackCooldownTimer > lightShadowAttackCooldown)
            {
                canLightShadowAttack = true;
                lightShadowAttackStarted = false;
                lightShadowAttackCooldownTimer = 0.0f;
            }
        }

        //------------------------

        hits = Physics2D.OverlapBoxAll(ceilingCheck.transform.position, ceilingCubeSize, 0f, groundLayer);

        isUnderCeiling = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                isUnderCeiling = true;
                break;
            }
        }

        //------------------------

        if (isUnderCeiling && moveDirection.x == 0f)
        {
            anim.SetBool("IsMoving", false);
            anim.SetBool("CanStandUp", false);
        }

        if(isUnderCeiling && moveDirection.x != 0f)
        {
            anim.SetBool("IsMoving", true);
            anim.SetBool("CanStandUp", false);
        }

        if(!isUnderCeiling)
        {
            anim.SetBool("CanStandUp", true);
        }

        //------------------------

        if (!startGroundedTimer)
        {
            UpdateIsGrounded();
        }

        //------------------------

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

        //------------------------

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            canCrouchMoveTimer = 0f;
            hasPressedCrouch = true;
        }

        if(hasPressedCrouch)
        {
            canCrouchMove = false;
            canCrouchMoveTimer += Time.deltaTime;

            if (canCrouchMoveTimer > 0.1f)
            {
                canCrouchMove = true;
                canCrouchMoveTimer = 0f;
                hasPressedCrouch = false;
            }
        }

        //------------------------

        if (Input.GetKey(KeyCode.S) && moveDirection.x == 0 && isGrounded)
        {
            crouchRisingTime = 0f;

            isCrouchMoving = false;

            anim.SetBool("IsCrouchIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCrouchMoving", false);
            crouchFallingTime += Time.deltaTime;
            isCrouchFalling = true;

            if (crouchFallingTime > 0.1f)
            {
                isCrouchFalling = false;
            }
        }

        else if (Input.GetKey(KeyCode.S) && moveDirection.x != 0 && isGrounded)
        {
            anim.SetBool("IsCrouchIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCrouchMoving", true);
            isCrouchMoving = true;
        }

        if (!Input.GetKey(KeyCode.S) && isGrounded)
        {
            crouchFallingTime = 0f;

            anim.SetBool("IsCrouchIdle", false);
            crouchRisingTime += Time.deltaTime;
            isCrouchRising = true;

            if (crouchRisingTime > 0.1f)
            {
                isCrouchRising = false;
            }
        }

        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            anim.SetBool("IsCrouchHeld", true);
        }
        else
        {
            anim.SetBool("IsCrouchHeld", false);
        }

        //------------------------

        if (Input.GetKeyDown(KeyCode.E) && !unhiding)
        {
            AttemptInteraction();
        }

        //------------------------

        UpdateDashUI();
        DashReset();

        //------------------------

        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f - redFlash, 1.0f - redFlash);
        redFlash -= Time.deltaTime;
        redFlash = Mathf.Clamp(redFlash, 0.0f, 1.0f);
        unhiding = false;

         PlayerPrefs.SetFloat("Health",  Stats.currentHealth);
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

            if (dashResetTimer >= dashResetTime)
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
        Gizmos.DrawWireCube(ceilingCheck.transform.position, ceilingCubeSize);
    }

    public void Damage(float amount, GameObject source)
    {
        redFlash = 1.0f;
        Stats.currentHealth -= amount;
        PlayerPrefs.SetFloat("Health",  Stats.currentHealth);
        if(Stats.currentHealth <= 0.0f)
        {
            respawnManager.RespawnPlayer();
        }
    }

    public void Attack(int index)
    {
        weapon.Attack(index);
    }

    public void PlayStepSound()
    {
        stepAudio.PlayRandomSound();
    }

    public void PlayAttackSound()
    {
        attackAudio.PlayRandomSound();
    }

    public void AttemptInteraction()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(colliders);

        foreach( Collider2D collider in colliders)
        {
            ExecuteEvents.Execute<IInteractableObject>(collider.gameObject, null, (message, data) => message.Interact(this.gameObject));
        }
    }

    public void UpdateIsGrounded()
    {
        isGrounded = false;
        List<Collider2D> collidersFound = new List<Collider2D>();
        groundCheck.GetComponent<Collider2D>().Overlap(collidersFound);
        foreach (Collider2D collider in collidersFound)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = true;
            }
        }
    }

    public void Heal(float amount)
    {
        Stats.currentHealth = Mathf.Clamp(Stats.currentHealth + amount, 0, Stats.initialHealth);
    }

    public void SetLayerCollision(int layer1, int layer2, bool enableCollision)
    {
        int mask = Physics2D.GetLayerCollisionMask(layer1);

        int layer2BitValue = 1 << layer2;

        if (enableCollision)
        {
            mask |= layer2BitValue;
        }
        else
        {
            mask &= ~layer2BitValue;
        }

        Physics2D.SetLayerCollisionMask(layer1, mask);
    }
}
