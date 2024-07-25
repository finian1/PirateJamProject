using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is idle.");

        player.anim.SetBool("IsJumpRising", false);
        player.anim.SetBool("IsJumpFalling", false);

        player.anim.SetBool("IsCrouchIdle", false);
        player.anim.SetBool("IsCrouchFalling", false);
        player.anim.SetBool("IsCrouchMoving", false);
        //player.anim.SetBool("IsCrouchRising", false);

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
        if (player.moveDirection.x == 0f)
        {
            player.anim.SetBool("IsJumpRising", false);
            player.anim.SetBool("IsJumpFalling", false);
            player.anim.SetBool("IsMoving", false);
            player.anim.SetBool("IsRunning", false);
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.moveDirection.x != 0f)
        {
            player.anim.SetBool("IsMoving", true);
            player.SwitchState(PlayerState.MOVING);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SwitchState(PlayerState.JUMPING);
        }
            
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            player.anim.SetBool("hasCrouchRisingFinished", true);
            player.anim.SetBool("hasCrouchFallingFinished", true);

        }

        if (player.rb.velocity.y < -1f)
        {
            player.anim.SetBool("IsJumpFalling", true);
        }
        else
        {
            player.anim.SetBool("IsJumpFalling", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && player.isGrounded)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.SwitchState(PlayerState.LIGHTATTACKING);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Attacking");
            player.weapon.Attack(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && player.currentDashCounter > player.minDashCounter)
        {
            player.SwitchState(PlayerState.DASHING);
        }

        
    }
}
