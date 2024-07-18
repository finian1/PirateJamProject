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
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (player.horizontalMovement != 0)
        {
            player.currentMovementSpeed = 8f;
            player.rb.velocity = new Vector2(player.horizontalMovement * player.currentMovementSpeed, player.rb.velocity.y);
        }

        if (player.isFacingRight && player.horizontalMovement < 0f || !player.isFacingRight && player.horizontalMovement > 0f)
        {
            //player.currentMovementSpeed = 8f;
            //player.rb.velocity = new Vector2(player.horizontalMovement * player.currentMovementSpeed, player.rb.velocity.y);

            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if (player.horizontalMovement == 0)
        {
            player.SwitchState(player.IdleState);
        }

        if(Input.GetButton("Jump"))
        {
            player.SwitchState(player.JumpingState);
        }

        if (Input.GetButton("Crouch"))
        {
            player.SwitchState(player.CrouchingState);
        }
    }

    void Flip(PlayerStateManager player)
    {
        if (player.isFacingRight && player.horizontalMovement < 0f || !player.isFacingRight && player.horizontalMovement > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.originalScale;
            localScale.x *= -1f;
            player.originalScale = localScale;
        }
    }

}
