using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is dashing.");

        //player.rb.velocity = new Vector2(player.rb.velocity.x, 0.0f);
        player.justDashed = true;
        player.dashCooldownTimer = 0f;
        player.dashPower = 60f;

        player.initialDashCounter = player.currentDashCounter;

        if (player.currentDashCounter > player.minDashCounter)
        {
            player.currentDashCounter--;
        }
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
            }
        }

        if (player.dashCooldownTimer >= player.originalDashCooldownTimer)
        {
            player.SwitchState(PlayerState.IDLE);
        }
    }

}
