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
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (player.horizontalMovement == 0f)
        {
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.horizontalMovement != 0)
        {
            player.currentMovementSpeed = 8f;
            player.rb.velocity = new Vector2(player.horizontalMovement * player.currentMovementSpeed, player.rb.velocity.y);
        }

        if (player.isFacingRight && player.horizontalMovement < 0f || !player.isFacingRight && player.horizontalMovement > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if (player.isGrounded)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (Input.GetButtonDown("Attack1"))
        {
            Debug.Log("Attacking");
            player.weapon.Attack(0);
        }
    }
}
