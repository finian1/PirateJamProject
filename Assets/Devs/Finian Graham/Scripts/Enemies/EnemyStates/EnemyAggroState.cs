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
            enemy.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else
        {
            enemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if(!enemy.vision.IsPlayerInFront(forwardVector))
        {
            enemy.movingRight = !enemy.movingRight;
        }

        if (!enemy.vision.canSeePlayer)
        {
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
