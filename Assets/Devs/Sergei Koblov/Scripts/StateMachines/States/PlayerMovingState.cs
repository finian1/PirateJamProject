using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is moving.");

        if (!player.hasCrouchFlipReset)
        {
            float heightDifference = player.originalScale.y - player.crouchScale.y;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + heightDifference * 0.8f, player.transform.position.z);

            if (player.isFacingRight)
            {
                player.currentScale = player.originalScale;
            }
            else if (!player.isFacingRight)
            {
                Vector3 currentScale = player.originalScale;
                currentScale.x *= -1f;
                player.currentScale = currentScale;
            }

            player.currentMovementSpeed = player.originalMovementSpeed;
            player.hasCrouchFlipReset = true;
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.moveDirection.x != 0)
        {
            player.currentMovementSpeed = 8f;
            player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
        }

        if (player.isFacingRight && player.moveDirection.x < 0f || !player.isFacingRight && player.moveDirection.x > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if (player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (player.jumpButtonPressed)
        {
            player.SwitchState(PlayerState.JUMPING);
        }

        if (player.crouchButtonPressed)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (player.fireButtonPressed1)
        {
            Debug.Log("Attacking");
            player.weapon.Attack(0);
        }
        if (player.fireButtonPressed2)
        {
            Debug.Log("Attacking");
            player.weapon.Attack(1);
        }
    }

}
