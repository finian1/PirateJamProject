using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyChargeState : EnemyBaseState
{
    bool chargingRight = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Running", true);
        Vector2 dirToTarget = enemy.vision.closestTarget.transform.position - enemy.gameObject.transform.position;
        dirToTarget.Normalize();

        if(Vector2.Dot(dirToTarget, enemy.gameObject.transform.right) > 0)
        {
            chargingRight = true;
        }
        else
        {
            chargingRight = false;
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Running", false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float chargeVar = 1000.0f;
        if (!chargingRight)
        {
            chargeVar = -chargeVar;
        }

        enemy.GetComponent<Rigidbody2D>().AddForceX(chargeVar * Time.deltaTime);
    }
}
