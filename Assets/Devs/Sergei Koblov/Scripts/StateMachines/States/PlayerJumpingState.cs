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

        //player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, 10f, -10f));

        if(player.rb.velocity.y < 0)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 1.005f);
        }

        if(player.rb.velocity.y < -30f)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, -30f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.currentJumpCount < player.maxJumpCount)
        {
            Debug.Log("Player double jumped.");
            player.jumpingPower = player.originalJumpingPower;
            player.currentJumpCount++;
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpingPower);
        }

        if (!Input.GetKey(KeyCode.Space) && player.rb.velocity.y > 0)
        {
            float airDeceleration = Mathf.MoveTowards(player.rb.velocity.y, 0.0f, Time.deltaTime * 200);
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attacking");
            player.weapon.Attack(0);
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

        if (player.isGrounded)
        {
            player.currentJumpCount = 0;
            player.SwitchState(PlayerState.IDLE);
        }
    }
}
