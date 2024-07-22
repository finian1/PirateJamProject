using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        if (!player.justDashed)
        {
            Debug.Log("Player is jumping.");
            //player.groundCheck.SetActive(false);
            player.startGroundedTimer = true;
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
        }
        else
        {
            //player.rb.velocity = new Vector2(player.rb.velocity.x, 0.0f);
            player.justDashed = false;
            return;
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if (player.groundCheck)
        //{
        //    if (player.rb.velocity.y > 0)
        //    {
        //        player.groundCheck.SetActive(false);
        //    }
        //    if (player.rb.velocity.y < 0)
        //    {
        //        player.groundCheck.SetActive(true);
        //    }
        //}

        if (player.jumpPress && player.currentJumpCount < player.maxJumpCount)
        {
            Debug.Log("Player double jumped.");
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
        }

        if (!player._playerInputSystem.jumpHold && player.rb.velocity.y > 0)
        {
            float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 100);
            player.rb.velocity = new Vector2(player.rb.velocity.x, airDeceleration);
        }

        if (player.moveDirection.x == 0f)
        {
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.moveDirection.x != 0)
        {
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

        if (player.fire1Press)
        {
            Debug.Log("Attacking");
            player.weapon.Attack(0);
        }

        if (player.fire2Press)
        {
            Debug.Log("Attacking");
            player.weapon.Attack(1);
        }

        if (player.dashPress && player.currentDashCounter > player.minDashCounter)
        {
            player.SwitchState(PlayerState.DASHING);
        }

        if (player.isGrounded)
        {
            player.currentJumpCount = 0;
            player.SwitchState(PlayerState.IDLE);
        }
    }
}
