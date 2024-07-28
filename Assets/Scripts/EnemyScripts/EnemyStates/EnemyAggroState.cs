using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Running", true);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Running", false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.groundCheck.IsGroundPresent(enemy.tag) && !enemy.wallCheck.IsGroundPresent(enemy.tag))
        {
            float finalSpeed = enemy.aggroSpeed;
            if (!enemy.movingRight)
            {
                finalSpeed = -finalSpeed;
            }
            enemy.GetComponent<Rigidbody2D>().MovePosition(enemy.transform.position + new Vector3(finalSpeed * Time.deltaTime, 0.0f, 0.0f));
        }

        Vector3 forwardVector = enemy.transform.right;
        if (!enemy.movingRight)
        {
            forwardVector = -forwardVector;
            enemy.transform.localScale = new Vector3(enemy.initialScale.x * -1.0f, enemy.initialScale.y * 1.0f, enemy.initialScale.z * 1.0f);
        }
        else
        {
            enemy.transform.localScale = enemy.initialScale;
        }

        //If player is behind enemy, turn around.
        if(enemy.vision.canSeeTarget)
        {
            Vector2 targetDirection = enemy.vision.closestTarget.transform.position - enemy.transform.position;
            float playerDist = targetDirection.magnitude;
            targetDirection.Normalize();
            if(Vector3.Dot(forwardVector, targetDirection) < 0)
            {
                enemy.movingRight = !enemy.movingRight;
            }
            if(playerDist < enemy.attackRange && enemy.timeSinceLastAttack >= enemy.attackCooldown)
            {
                enemy.SwitchState(EnemyState.ATTACKING);
            }
        }

        if (!enemy.vision.canSeeTarget)
        {
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
