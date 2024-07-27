using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowAttackState : EnemyBaseState
{
    private float attackTimer = 0.0f;
    private Vector3 targetLocation;
    private Vector3 initLocation;

    private float startJumpTime = 0.16675f;
    private float endJumpTime = 0.80025f;


    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.timeSinceLastAttack = 0.0f;
        initLocation = enemy.transform.position;
        enemy.animator.SetBool("Attacking", true);
        if (enemy.movingRight)
        {
            targetLocation = enemy.transform.position + new Vector3(((SmallShadowStateManager)enemy).lungeDistance, 0.0f, 0.0f);
        }
        else
        {
            targetLocation = enemy.transform.position - new Vector3(((SmallShadowStateManager)enemy).lungeDistance, 0.0f, 0.0f);
        }

        if (!enemy.movingRight)
        {
            enemy.transform.localScale = new Vector3(enemy.initialScale.x * -1.0f, enemy.initialScale.y * 1.0f, enemy.initialScale.z * 1.0f);
        }
        else
        {
            enemy.transform.localScale = enemy.initialScale;
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("Attacking", false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float lerpVal = (attackTimer - startJumpTime) / (endJumpTime - startJumpTime);

        if (attackTimer > startJumpTime && attackTimer < endJumpTime)
        {
            Vector3 newPos = Vector3.Lerp(initLocation, targetLocation, lerpVal);
            enemy.GetComponent<Rigidbody2D>().MovePosition(newPos);

            List<Collider2D> targets = new List<Collider2D>();
            enemy.attackArea.Overlap(targets);

            foreach (Collider2D target in targets)
            {
                ExecuteEvents.Execute<IDamageableObject>(target.gameObject, null, (message, data) => message.Damage(enemy.attackDamage, enemy.gameObject));
            }
        }

        if (attackTimer >= enemy.attackAnimationLength)
        {
            enemy.SwitchState(EnemyState.ROAMING);
            attackTimer = 0.0f;
        }

        attackTimer += Time.deltaTime;
    }
}
