using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHidingState : EnemyBaseState
{
    private float timer = 0.0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        timer = 0.0f;
        enemy.animator.SetBool("Hiding", true);
        enemy.GetComponent<Rigidbody2D>().simulated = false;
        enemy.bodyCollider.isTrigger = true;
        enemy.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Hiding", false);
        //enemy.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        timer += Time.deltaTime;
        if (timer >= ((SmallShadowStateManager)enemy).timeToHide)
        {
            enemy.SwitchState(EnemyState.HIDDEN);
        }
    }
}
