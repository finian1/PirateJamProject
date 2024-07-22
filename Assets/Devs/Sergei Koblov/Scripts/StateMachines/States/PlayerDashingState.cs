using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is dashing.");

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
            if (player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
            {
                player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                player.rb.velocity = new Vector2(player.dashPower, player.rb.velocity.y);
            }

            if (!player.isFacingRight && player.dashCooldownTimer < player.originalDashCooldownTimer)
            {
                player.dashPower = Mathf.MoveTowards(player.dashPower, 0f, Time.deltaTime * (player.dashPower * 8f));
                player.rb.velocity = new Vector2(-player.dashPower, player.rb.velocity.y);
            }
        }

        if (player.dashCooldownTimer >= player.originalDashCooldownTimer)
        {
            player.SwitchState(PlayerState.IDLE);
        }
    }

}
