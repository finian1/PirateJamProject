using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowUnhidingState : EnemyBaseState
{

    private float timer = 0.0f;
    public override void EnterState(EnemyStateManager enemy)
    {
        timer = 0.0f;
        enemy.GetComponent<SpriteRenderer>().enabled = true;
        enemy.animator.SetBool("Unhiding", true);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Unhiding", false);
        enemy.GetComponent<Rigidbody2D>().simulated = true;
        enemy.bodyCollider.isTrigger = false;
        enemy.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        timer += Time.deltaTime;
        if(timer >= ((SmallShadowStateManager)enemy).timeToHide)
        {
            enemy.SwitchState(EnemyState.ROAMING);
        }
    }
}
