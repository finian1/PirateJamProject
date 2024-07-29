using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLightAlchemistRoamingState : EnemyRoamingState
{
    private float timeSinceLastInvestigation = 0.0f;
    private float timeUntilNextInvestigation = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        base.EnterState(enemy);
        timeUntilNextInvestigation = Random.Range(((SmallLightAlchemistStateManager)enemy).minTimeBetweenInvestigation, ((SmallLightAlchemistStateManager)enemy).maxTimeBetweenInvestigation);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //While roaming, the small light alchemist will stop and look around for a small amount of time before continuing
        timeSinceLastInvestigation += Time.deltaTime;
        if (timeSinceLastInvestigation >= timeUntilNextInvestigation)
        {
            timeSinceLastInvestigation = 0.0f;
            enemy.SwitchState(EnemyState.INVESTIGATING);
        }
        base.UpdateState(enemy);
    }
}
