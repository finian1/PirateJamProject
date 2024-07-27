using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRoamingState : EnemyRoamingState
{
    float timeToHideTimer = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        base.EnterState(enemy);
        enemy.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        base.ExitState(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        base.UpdateState(enemy);
        if(enemy.vision.canSeeTarget)
        {
            timeToHideTimer = 0.0f;
        }
        else
        {
            timeToHideTimer += Time.deltaTime;
        }

        if(timeToHideTimer >= ((SmallShadowStateManager)enemy).timeBeforeHiding)
        {
            enemy.SwitchState(EnemyState.HIDING);
        }
    }
}
