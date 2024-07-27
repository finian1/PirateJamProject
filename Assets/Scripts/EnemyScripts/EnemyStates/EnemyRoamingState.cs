using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
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
        
        if(!enemy.groundCheck.IsGroundPresent(enemy.tag) || enemy.wallCheck.IsGroundPresent(enemy.tag))
        {
            enemy.movingRight = !enemy.movingRight;
        }

        float finalSpeed = enemy.movementSpeed;
        if (!enemy.movingRight)
        {
            finalSpeed = -finalSpeed;

            enemy.transform.localScale = new Vector3(enemy.initialScale.x * -1.0f, enemy.initialScale.y * 1.0f, enemy.initialScale.z * 1.0f);
        }
        else
        {
            enemy.transform.localScale = enemy.initialScale;
        }

        enemy.GetComponent<Rigidbody2D>().MovePosition(enemy.transform.position + new Vector3(finalSpeed * Time.deltaTime, 0.0f, 0.0f));

        Vector3 forwardVector = enemy.transform.right;
        if(!enemy.movingRight) 
        {
            forwardVector = -forwardVector;
        }

        if(enemy.vision.canSeeTarget)
        {
            enemy.SwitchState(EnemyState.AGGRO);
        }
    }
}
