using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttackingState : EnemyBaseState
{
    private float attackWindupTimer = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        attackWindupTimer = 0.0f;
        enemy.timeSinceLastAttack = 0.0f;
        enemy.animator.SetBool("Attacking", true);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Attacking", false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        attackWindupTimer += Time.deltaTime;
        if(attackWindupTimer >= enemy.attackSpeed)
        {
            if (enemy.vision.canSeeTarget && enemy.vision.closestTarget != null)
            {
                Vector3 targetDirection = enemy.vision.closestTarget.transform.position - enemy.transform.position;
                float targetDist = targetDirection.magnitude;
                if (targetDist <= enemy.attackRange)
                {
                    enemy.SwitchState(EnemyState.ATTACKING);
                }
                else
                {
                    enemy.SwitchState(EnemyState.AGGRO);
                }
            }
            else
            {
                enemy.SwitchState(EnemyState.ROAMING);
            }
        }
    }
}
