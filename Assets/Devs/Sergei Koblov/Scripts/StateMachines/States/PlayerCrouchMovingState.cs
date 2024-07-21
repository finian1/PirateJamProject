using UnityEngine;

public class PlayerCrouchMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is crouch moving.");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.moveDirection.x == 0f)
        {
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.moveDirection.x != 0)
        {
            player.currentMovementSpeed = player.crouchingMovementSpeed;
            player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
        }

        float heightDifference = player.currentScale.y - player.crouchScale.y;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - heightDifference * 0.8f, player.transform.position.z);
        player.currentMovementSpeed = player.originalMovementSpeed;

        if (player.isFacingRight)
        {
            player.currentScale = player.crouchScale;
        }

        else if (!player.isFacingRight)
        {
            Vector3 currentScale = player.crouchScale;
            currentScale.x *= -1f;
            player.currentScale = currentScale;
        }

        if (player.isFacingRight && player.moveDirection.x < 0f || !player.isFacingRight && player.moveDirection.x > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if (!player._playerInputSystem.crouchHold && player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (!player._playerInputSystem.crouchHold && player.moveDirection.x != 0)
        {
            player.SwitchState(PlayerState.MOVING);
        }

        if( player._playerInputSystem.crouchHold && player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }
    }
}
