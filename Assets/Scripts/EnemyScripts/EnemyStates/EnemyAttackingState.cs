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
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        attackWindupTimer += Time.deltaTime;
        if(attackWindupTimer >= enemy.attackSpeed)
        {
            Attack(enemy);
            
            if (enemy.vision.canSeeTarget)
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

    private void Attack(EnemyStateManager enemy)
    {
        Debug.DrawLine(enemy.transform.position, enemy.transform.position + enemy.forwardVector * enemy.attackRange, Color.cyan, 1.0f);
        List<Collider2D> targets = new List<Collider2D>();
        enemy.attackArea.Overlap(targets);

        foreach (Collider2D target in targets)
        {
            ExecuteEvents.Execute<IDamageableObject>(target.gameObject, null, (message, data) => message.Damage(enemy.attackDamage, enemy.gameObject));
        }
    }
}
