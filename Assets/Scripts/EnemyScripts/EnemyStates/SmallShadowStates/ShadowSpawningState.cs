using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawningState : EnemyBaseState
{
    private float timeToSpawnTimer = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.GetComponent<Rigidbody2D>().simulated = false;
        enemy.bodyCollider.isTrigger = true;
        enemy.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        enemy.animator.SetBool("Spawning", true);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.GetComponent<Rigidbody2D>().simulated = true;
        enemy.bodyCollider.isTrigger = false;
        enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
        enemy.animator.SetBool("Spawning", false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        timeToSpawnTimer += Time.deltaTime;
        if(timeToSpawnTimer > ((SmallShadowStateManager)enemy).timeToSpawn)
        {
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
