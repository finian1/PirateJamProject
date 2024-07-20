using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    ROAMING,
    AGGRO
}

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState currentEnemyState;
    //Variables
    public VisionScript vision;
    public GroundCheckScript groundCheck;
    public GroundCheckScript wallCheck;

    public float movementSpeed = 10.0f;
    public float aggroSpeed = 15.0f;

    public bool movingRight = false;

    public Vector3 initialScale;


    public Dictionary<EnemyState, EnemyBaseState> EnemyStates = new Dictionary<EnemyState, EnemyBaseState>()
    {
        {EnemyState.IDLE, new EnemyIdleState() },
        {EnemyState.ROAMING, new EnemyRoamingState() },
        {EnemyState.AGGRO, new EnemyAggroState() }
    };

    private void Start()
    {
        initialScale = transform.localScale;
        SwitchState(EnemyState.ROAMING);
    }

    private void Update()
    {
        currentEnemyState.UpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        currentEnemyState = EnemyStates[state];
        currentEnemyState.EnterState(this);
    }
}
