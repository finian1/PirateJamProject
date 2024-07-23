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

        player.anim.SetBool("IsRunning", true);

    }

    public override void UpdateState(PlayerStateManager player)
    {
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

        if (player.moveDirection.x == 0)
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //player.groundCheck.SetActive(false);
            player.SwitchState(PlayerState.JUMPING);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            player.SwitchState(PlayerState.CROUCHING);
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
    }

}
