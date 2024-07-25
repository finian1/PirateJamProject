using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    HIDING
}

public class EnemyStateManager : MonoBehaviour, IInteractionEvents
{
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
    public float attackDamage = 20.0f;
    public float attackRange = 2.0f;
    public Collider2D attackArea;

    [NonSerialized]
    public bool movingRight = false;
    [NonSerialized]
    public Vector3 initialScale;
    [NonSerialized]
    public Vector3 forwardVector;


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
        initialScale = transform.localScale;
        currentEnemyHealth = initialEnemyHealth;
        SwitchState(EnemyState.ROAMING);
    }

    private void FixedUpdate()
    {
        EnemyStates[currentEnemyState].UpdateState(this);
        if(movingRight)
        {
            forwardVector = transform.right;
        }
        else
        {
            forwardVector = -transform.right;
        }
    }

    public void SwitchState(EnemyState state)
    {
        previousEnemyState = currentEnemyState;
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
        SwitchState(EnemyState.DAMAGED);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
