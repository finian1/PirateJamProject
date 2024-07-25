using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLightAlchemistInvestigatingState : EnemyBaseState
{
    private float visionFlipTimer = 0.0f;
    private float investigationTimer = 0.0f;
    private float currentInvestigationTime = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        currentInvestigationTime = Random.Range(((SmallLightAlchemistStateManager)enemy).minInvestigationTime, ((SmallLightAlchemistStateManager)enemy).maxInvestigationTime);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        visionFlipTimer += Time.deltaTime;
        investigationTimer += Time.deltaTime;

        if(visionFlipTimer >= ((SmallLightAlchemistStateManager)enemy).timeBetweenViewFlips)
        {
            enemy.movingRight = !enemy.movingRight;
            if (!enemy.movingRight)
            {
                enemy.transform.localScale = new Vector3(enemy.initialScale.x * -1.0f, enemy.initialScale.y * 1.0f, enemy.initialScale.z * 1.0f);
            }
            else
            {
                enemy.transform.localScale = enemy.initialScale;
            }
            visionFlipTimer = 0.0f;
        }

        if(enemy.vision.canSeeTarget)
        {
            enemy.SwitchState(EnemyState.AGGRO);
        }
        else if(investigationTimer >= currentInvestigationTime)
        {
            investigationTimer = 0.0f;
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
