using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum EnemyState
{
    IDLE,
    ROAMING,
    AGGRO,
    ATTACKING,
    SLEEPING,
    DISTRACTED,
    INVESTIGATING,
    DAMAGED,
    HIDDEN,
    HIDING,
    UNHIDING,
    SPAWNING,
}

public class EnemyStateManager : MonoBehaviour, IDamageableObject
{
    public GameObject spawnPrefab;

    public EnemyState previousEnemyState;
    public EnemyState currentEnemyState;
    //Variables
    public VisionScript vision;
    public GroundCheckScript groundCheck;
    public GroundCheckScript wallCheck;

    public float movementSpeed = 10.0f;
    public float aggroSpeed = 15.0f;
    public float initialEnemyHealth = 25.0f;
    [NonSerialized]
    public float currentEnemyHealth;

    [Header("Attack Variables")]
    public float attackSpeed = 2.5f;
    public float attackCooldown = 2.0f;
    public float attackDamage = 20.0f;
    public float attackRange = 2.0f;
    public float attackAnimationLength = 1.0f;
    public Collider2D attackArea;

    [NonSerialized]
    public bool movingRight = false;
    [NonSerialized]
    public Vector3 initialScale;
    [NonSerialized]
    public Vector3 forwardVector;
    public Animator animator;
    [NonSerialized]
    public float timeSinceLastAttack;
    public bool shouldPlaceSpawner = true;
    public RandomAudioScript stepAudio;
    public Collider2D bodyCollider;


    public Dictionary<EnemyState, EnemyBaseState> EnemyStates = new Dictionary<EnemyState, EnemyBaseState>()
    {
        {EnemyState.IDLE, new EnemyIdleState() },
        {EnemyState.ROAMING, new EnemyRoamingState() },
        {EnemyState.AGGRO, new EnemyAggroState() },
        {EnemyState.ATTACKING, new EnemyAttackingState() },
        {EnemyState.DAMAGED, new EnemyDamagedState() }
    };

    public virtual void Start()
    {
        movingRight = true;
        if (shouldPlaceSpawner && spawnPrefab != null)
        {
            Instantiate(spawnPrefab, transform.position, transform.rotation);
        }

        bodyCollider = GetComponent<Collider2D>();
    }

    public virtual void Awake()
    {
        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        initialScale = transform.localScale;
        currentEnemyHealth = initialEnemyHealth;
        SwitchState(EnemyState.ROAMING);
    }

    public virtual void FixedUpdate()
    {
        timeSinceLastAttack += Time.deltaTime;
        EnemyStates[currentEnemyState].UpdateState(this);
        if(movingRight)
        {
            forwardVector = transform.right;
        }
        else
        {
            forwardVector = -transform.right;
        }

        GetComponent<Rigidbody2D>().velocity *= new Vector3(0.0f, 1.0f, 0.0f);
    }

    public void SwitchState(EnemyState state)
    {
        previousEnemyState = currentEnemyState;
        EnemyStates[currentEnemyState].ExitState(this);
        currentEnemyState = state;
        EnemyStates[currentEnemyState].EnterState(this);
    }

    public void ReturnToPreviousState()
    {
        SwitchState(previousEnemyState);
    }

    public virtual void Damage(float amount, GameObject origin)
    {
        currentEnemyHealth -= amount;
        if (currentEnemyHealth <= 0.0f)
        {
            Die();
        }
        //SwitchState(EnemyState.DAMAGED);
    }

    public void PlayStepAudio()
    {
        stepAudio.PlayRandomSound();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
