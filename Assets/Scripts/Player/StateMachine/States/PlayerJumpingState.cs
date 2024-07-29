using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        if (player.coyoteTimeCounter > 0f && player.currentJumpCount < player.maxJumpCount && !player.justDashed && !player.justLightAttacked)
        {   
            Debug.Log("Player is jumping.");
            player.anim.SetBool("IsRunning", false);
            player.isJumpBuffering = false;
            player.startGroundedTimer = true;
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
        }
        if(player.justDashed == true)
        {
            player.justDashed = false;
            return;
        }
        if(player.justLightAttacked == true)
        {
            player.justLightAttacked = false;
            return;
        }
        if(player.justLightShadowAttacked == true)
        {
            player.justLightShadowAttacked = false;
            return;
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(player.rb.velocity.y > 0f)
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

        if(player.rb.velocity.y < -40f)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, -40f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.currentJumpCount == 0)
        {
            Debug.Log("Player first jumped.");
            //player.jumpBufferCounter = 0f;
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.currentJumpCount == 1)
        {
            Debug.Log("Player double jumped.");
            //player.jumpBufferCounter = 0f;
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
            player._playerDoubleJumpParticle.doubleJumpParticlesPlaying = true;
        }

        else if (player.currentJumpCount == 2 && player.jumpBufferCounter > 0f && player.isGrounded)
        {
            player.jumpingPower = player.originalJumpingPower;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
            player.currentJumpCount = 1;
            //player.jumpBufferCounter = 0f;
            //player.SwitchState(PlayerState.JUMPING);
        }

        if (!Input.GetKey(KeyCode.Space) && player.rb.velocity.y > 0)
        {
            float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);
            player.rb.velocity = new Vector2(player.rb.velocity.x, airDeceleration);
        }

        if (player.moveDirection.x == 0f)
        {
            player.anim.SetBool("IsMoving", false);
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.moveDirection.x != 0)
        {
            player.anim.SetBool("IsMoving", true);
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

        if (player.isGrounded && !player.isJumpBuffering)
        {
            Debug.Log("Going Idle.");
            player.currentJumpCount = 0;
            player.SwitchState(PlayerState.IDLE);
        }

    }
}
