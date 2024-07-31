using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHiddenState : EnemyBaseState
{
    private float targetSpottedTimer = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        targetSpottedTimer = 0.0f;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(enemy.vision.canSeeTarget)
        {
            targetSpottedTimer += Time.deltaTime;
            if(targetSpottedTimer >= ((SmallShadowStateManager)enemy).timeBeforeAmbush)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                enemy.SwitchState(EnemyState.UNHIDING);
            }
        }
        else
        {
            targetSpottedTimer -= Time.deltaTime;
            targetSpottedTimer = Mathf.Clamp(targetSpottedTimer, 0.0f, ((SmallShadowStateManager)enemy).timeBeforeAmbush);
        }
    }
}
