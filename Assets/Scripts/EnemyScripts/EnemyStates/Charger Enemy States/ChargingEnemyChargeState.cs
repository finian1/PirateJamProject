using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyChargeState : EnemyBaseState
{
    bool chargingRight = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        Vector2 dirToPlayer = enemy.vision.playerObject.transform.position - enemy.gameObject.transform.position;
        dirToPlayer.Normalize();

        if(Vector2.Dot(dirToPlayer, enemy.gameObject.transform.right) > 0)
        {
            chargingRight = true;
        }
        else
        {
            chargingRight = false;
        }
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
