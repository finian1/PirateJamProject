using UnityEngine;

public class PlayerCrouchingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is crouching.");
        player.hasCrouchFlipReset = false;

        player.anim.SetBool("IsRunning", false);

        player.anim.SetBool("IsCrouchIdle", false);
        player.anim.SetBool("IsCrouchFalling", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.currentMovementSpeed = 0f;

        //float heightDifference = player.currentScale.y - player.crouchScale.y;
        //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - heightDifference * 0.8f, player.transform.position.z);
        player.currentMovementSpeed = player.originalMovementSpeed;

        if(Input.GetKey(KeyCode.LeftControl) && player.moveDirection.x == 0)
        {
            player.anim.SetBool("IsMoving", false);
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
            player.anim.SetBool("IsCrouchIdle", true);
            //player.anim.SetBool("IsCrouchFalling", false);
        }

        if (player.isFacingRight)
        {
            player.currentScale = player.crouchScale;
        }

        if(player.isGrounded)
        {
            player.anim.SetBool("IsJumpFalling", false);
        }

        if(Input.GetKey(KeyCode.LeftControl) && !player.isGrounded)
        {
            player.anim.SetBool("IsCrouchFalling", false);
            player.anim.SetBool("IsJumpFalling", true);
        }
        
        //if (player.isGrounded)
        //{
        //    player.anim.SetBool("IsJumpFalling", false);
        //    player.anim.SetBool("IsRunning", false);
        //    player.anim.SetBool("IsCrouchFalling", true);

        //    //player.anim.SetBool("IsCrouchIdle", false);
        //}

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
            
        if (!Input.GetKey(KeyCode.LeftControl) && player.isGrounded)
        {
            player.anim.SetBool("IsCrouchIdle", false);
            //player.anim.SetBool("IsCrouchFalling", false);

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

        if (Input.GetKey(KeyCode.LeftControl) && player.moveDirection.x != 0f)
        {
            player.SwitchState(PlayerState.CROUCHMOVING);
        }
    }
}
