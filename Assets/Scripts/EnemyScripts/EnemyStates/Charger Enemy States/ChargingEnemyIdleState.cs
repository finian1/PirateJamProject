using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(enemy.vision.canSeeTarget)
        {
            enemy.SwitchState(EnemyState.AGGRO);
        }
    }
}
