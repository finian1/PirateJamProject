using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightShadowAttackingState : PlayerBaseState
{

    float attackDuration;

    public override void EnterState(PlayerStateManager player)
    {

        Debug.Log("Player is light shadow attacking.");
        player.anim.SetBool("IsLightShadowAttacking", true);


        if (player.isFacingRight && player.mousePosition.x < player.transform.position.x || !player.isFacingRight && player.mousePosition.x > player.transform.position.x)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        player.weapon.Attack(1);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        attackDuration += Time.deltaTime;

        player.rb.velocity = new Vector2(0.0f, 0.0f);
        player.lightShadowAttackStarted = true;

        if (attackDuration > 0.25f)
        {
            if(!player.isGrounded)
            {
                attackDuration = 0f;
                //player.justLightShadowAttacked = true;
                player.anim.SetBool("IsLightShadowAttacking", false);
                player.SwitchState(PlayerState.JUMPING);
            }
            if(player.isGrounded)
            {
                attackDuration = 0f;
                //player.justLightShadowAttacked = true;
                player.anim.SetBool("IsLightShadowAttacking", false);
                player.SwitchState(PlayerState.IDLE);
            }
        }
    }
}
