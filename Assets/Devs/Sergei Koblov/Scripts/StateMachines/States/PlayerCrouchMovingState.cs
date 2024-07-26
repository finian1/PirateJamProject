using UnityEngine;

public class PlayerCrouchMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is crouch moving.");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if (player.moveDirection.x == 0f)
        //{
        //    player.anim.SetBool("IsCrouchIdle", true);
        //    player.anim.SetBool("IsCrouchMoving", false);
        //    player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        //}

        if (player.moveDirection.x != 0)
        {
            player.anim.SetBool("IsMoving", true);
            player.currentMovementSpeed = player.crouchingMovementSpeed;
            player.rb.velocity = new Vector2(player.moveDirection.x * player.currentMovementSpeed, player.rb.velocity.y);
        }

        //if (!player.isGrounded)
        //{
        //    player.anim.SetBool("IsJumpFalling", true);
        //}

        //if (player.isGrounded)
        //{
        //    player.anim.SetBool("IsJumpFalling", false);
        //}

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

        if (!Input.GetKey(KeyCode.LeftControl) && player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (!player.isGrounded && player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (!player.isGrounded && player.moveDirection.x != 0)
        {
            player.SwitchState(PlayerState.MOVING);
        }

        if (!Input.GetKey(KeyCode.LeftControl) && player.moveDirection.x != 0)
        {
            player.SwitchState(PlayerState.MOVING);
        }

        if(Input.GetKey(KeyCode.LeftControl) && player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        
    }
}
