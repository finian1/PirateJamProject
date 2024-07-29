using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is moving.");

        player.anim.SetBool("IsCrouchIdle", false);

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
        if (player.moveDirection.x != 0 && player.isGrounded)
        {
            player.anim.SetBool("IsMoving", true);
            player.anim.SetBool("IsRunning", true);
            player.currentMovementSpeed = player.originalMovementSpeed;
            player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
        }

        if (player.isFacingRight && player.moveDirection.x < 0f || !player.isFacingRight && player.moveDirection.x > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if(!player.isGrounded && player.moveDirection.x != 0)
        {
            player.anim.SetBool("IsMoving", true);
            player.anim.SetBool("IsRunning", true);
            player.currentMovementSpeed = player.originalMovementSpeed;
            player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
        }

        if (player.rb.velocity.y < -0.0f && !player.isGrounded)
        {
            player.anim.SetBool("IsJumpFalling", true);
        }
        else
        {
            player.anim.SetBool("IsJumpFalling", false);
        }

        if (player.moveDirection.x == 0)
        {
            player.anim.SetBool("IsMoving", false);
            player.anim.SetBool("IsRunning", false);
            player.SwitchState(PlayerState.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SwitchState(PlayerState.JUMPING);
        }

        if (Input.GetKey(KeyCode.LeftControl) && player.isGrounded && player.moveDirection.x == 0f)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (player.isUnderCeiling)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (Input.GetKey(KeyCode.LeftControl) && player.isGrounded && player.moveDirection.x != 0f)
        {
            player.SwitchState(PlayerState.CROUCHMOVING);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.SwitchState(PlayerState.LIGHTATTACKING);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && player.canLightShadowAttack)
        {
            player.SwitchState(PlayerState.LIGHTSHADOWATTACKING);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && player.currentDashCounter > player.minDashCounter)
        {
            player.SwitchState(PlayerState.DASHING);
        }
    }
}
