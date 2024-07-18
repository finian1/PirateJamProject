using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is jumping.");
        player.jumpingPower = 16f;
        player.jumpCount++;
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(player.isGrounded)
        {
            player.SwitchState(player.IdleState);
        }
    }
}
