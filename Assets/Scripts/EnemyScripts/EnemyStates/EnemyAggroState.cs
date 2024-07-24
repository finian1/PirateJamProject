using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.groundCheck.IsGroundPresent() && !enemy.wallCheck.IsGroundPresent())
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
        if(enemy.vision.canSeePlayer)
        {
            Vector3 playerDirection = enemy.vision.playerObject.transform.position - enemy.transform.position;
            float playerDist = playerDirection.magnitude;
            playerDirection.Normalize();
            if(Vector3.Dot(forwardVector, playerDirection) < 0)
            {
                enemy.movingRight = !enemy.movingRight;
            }
            if(playerDist < enemy.attackRange)
            {
                enemy.SwitchState(EnemyState.ATTACKING);
            }
        }

        if (!enemy.vision.canSeePlayer)
        {
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
