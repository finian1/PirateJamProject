using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : EnemyBaseState
{
    float deathAnimationTimer = 0.0f;
    bool finishedDamage = false;
    Animator animator;

    public override void EnterState(EnemyStateManager enemy)
    {
        animator = enemy.GetComponent<Animator>();
        if(animator != null)
        {
            animator.SetBool("Dying", true);
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(animator != null)
        {
            deathAnimationTimer += Time.deltaTime;
            if(deathAnimationTimer >= animator.GetCurrentAnimatorStateInfo(0).length)
            {
                finishedDamage = true;
            }
        }
        else
        {
            finishedDamage = true;
        }

        if(finishedDamage)
        {
            if(enemy.currentEnemyHealth <= 0.0f)
            {
                enemy.Die();
            }
            else
            {
                enemy.ReturnToPreviousState();
            }
        }
    }
}
