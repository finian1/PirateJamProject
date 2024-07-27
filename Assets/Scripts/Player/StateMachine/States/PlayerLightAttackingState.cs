using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightAttackingState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is light attacking.");
        //player.justLightAttacked = true;
        player.anim.SetBool("IsLightAttacking", true);
        //player.rb.velocity = new Vector2(0.0f, 0.0f);

        if (player.isFacingRight && player.mousePosition.x < player.transform.position.x || !player.isFacingRight && player.mousePosition.x > player.transform.position.x)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //float VelocityY = player.rb.velocity.y * 0.25f;
        //float VelocityX = player.rb.velocity.x * 0.25f;

        player.lightAttackCooldown += Time.deltaTime;

        if(!player.isGrounded)
        {
            if (player.rb.velocity.y < 0)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 1.005f);
            }

            if (player.rb.velocity.y < -30f)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, -30f);
            }

            if (player.moveDirection.x == 0f)
            {
                player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
            }

            if (player.moveDirection.x != 0)
            {
                player.currentMovementSpeed = player.originalMovementSpeed;
                player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
            }

            if(player.lightAttackCooldown > 0.25f)
            {
                player.lightAttackCooldown = 0f;
                player.justLightAttacked = true;
                player.anim.SetBool("IsLightAttacking", false);
                player.SwitchState(PlayerState.JUMPING);
            }
        }

        if(player.isGrounded)
        {
            if (player.rb.velocity.y < 0)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 1.005f);
            }

            if (player.rb.velocity.y < -30f)
            {
                player.rb.velocity = new Vector2(player.rb.velocity.x, -30f);
            }

            if (player.moveDirection.x == 0f)
            {
                player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
            }

            if (player.moveDirection.x != 0)
            {
                player.currentMovementSpeed = player.originalMovementSpeed;
                player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
            }

            if (player.lightAttackCooldown > 0.25f)
            {
                player.lightAttackCooldown = 0f;
                player.anim.SetBool("IsLightAttacking", false);
                player.SwitchState(PlayerState.IDLE);
            }
        }

        
    }

}
