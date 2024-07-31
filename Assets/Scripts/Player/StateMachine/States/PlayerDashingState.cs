using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is dashing.");

        player._playerDashParticle.dashParticlesPlaying = true;
        player.dashCooldownTimer = 0f;
        player.dashPower = 80f;

        player.rb.velocity = new Vector2(player.rb.velocity.x, 0.0f);

        player.initialDashCounter = player.currentDashCounter;

        if (player.currentDashCounter > player.minDashCounter)
        {
            player.currentDashCounter--;
        }

        player.layerActive = true;

        player.anim.SetBool("IsDashing", true);

        Stats.currentCorruption -= 2.5f;

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.initialDashCounter > player.minDashCounter)
        {
            if(player.isGrounded)
            {
                if (player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
                {
                    float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);

                    player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                    player.rb.velocity = new Vector2(player.dashPower, airDeceleration);
                }

                if (!player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
                {
                    float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);

                    player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                    player.rb.velocity = new Vector2(-player.dashPower, airDeceleration);
                }

                if(player.dashCooldownTimer >= player.originalDashCooldownTimer && player.moveDirection.x != 0f)
                {
                    //player.SetLayerCollision(player.playerLayer, player.enemyLayer, true);
                    player.anim.SetBool("IsDashing", false);
                    player.SwitchState(PlayerState.MOVING);
                }

                if (player.dashCooldownTimer >= player.originalDashCooldownTimer)
                {
                    //player.SetLayerCollision(player.playerLayer, player.enemyLayer, true);
                    player.anim.SetBool("IsDashing", false);
                    player.SwitchState(PlayerState.IDLE);
                }

            }

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

                if (player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
                {
                    float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);

                    player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                    player.rb.velocity = new Vector2(player.dashPower, airDeceleration);
                }

                if (!player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
                {
                    float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);

                    player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                    player.rb.velocity = new Vector2(-player.dashPower, airDeceleration);
                }

                if (player.dashCooldownTimer >= player.originalDashCooldownTimer && player.moveDirection.x != 0f)
                {
                    //player.SetLayerCollision(player.playerLayer, player.enemyLayer, true);
                    player.anim.SetBool("IsDashing", false);
                    player.SwitchState(PlayerState.MOVING);
                }

                if (player.dashCooldownTimer >= player.originalDashCooldownTimer)
                {
                    //player.SetLayerCollision(player.playerLayer, player.enemyLayer, true);
                    player.justDashed = true;
                    player.anim.SetBool("IsDashing", false);
                    player.SwitchState(PlayerState.JUMPING);
                }
            }
        }

        
    }

}
