using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is idle.");

        if(player.isGrounded)
        {
            player.anim.SetBool("IsJumpRising", false);
            player.anim.SetBool("IsJumpFalling", false);
        }

        player.anim.SetBool("IsCrouchIdle", false);
        player.anim.SetBool("IsCrouchMoving", false);

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
        if (player.moveDirection.x == 0f && player.isGrounded)
        {
            player.anim.SetBool("IsJumpRising", false);
            player.anim.SetBool("IsJumpFalling", false);
            player.anim.SetBool("IsMoving", false);
            player.anim.SetBool("IsRunning", false);
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.moveDirection.x != 0f && player.isGrounded)
        {
            player.anim.SetBool("IsMoving", true);
            player.SwitchState(PlayerState.MOVING);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SwitchState(PlayerState.JUMPING);
        }
            
        if(!player.isGrounded)
        {

            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);

            if (player.rb.velocity.y > 0f)
            {
                player.anim.SetBool("IsJumpRising", true);
                player.anim.SetBool("IsJumpFalling", false);
            }

            if (player.rb.velocity.y < 0f)
            {
                player.anim.SetBool("IsJumpRising", false);
                player.anim.SetBool("IsJumpFalling", true);
                player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 1.005f);
            }

            if (player.moveDirection.x != 0f)
            {
                player.anim.SetBool("IsMoving", true);
                player.SwitchState(PlayerState.MOVING);
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && player.isGrounded)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (player.isUnderCeiling)
        {
            player.SwitchState(PlayerState.CROUCHING);
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
