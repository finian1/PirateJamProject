using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyStateManager : EnemyStateManager
{

    //Example enemy state manager for an enemy that has no roaming state 
    private void Start()
    {
        EnemyStates = new Dictionary<EnemyState, EnemyBaseState>()
        {
            {EnemyState.IDLE, new ChargingEnemyIdleState() },
            {EnemyState.AGGRO, new ChargingEnemyChargeState() }
        };


        SwitchState(EnemyState.IDLE);
    }
}
