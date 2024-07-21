using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is idle.");

        if(!player.hasCrouchFlipReset)
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

        player.animator.SetBool("IsRunning", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        if(player.horizontalMovement == 0f)
        {
            player.rb.velocity = new Vector2(0.0f, player.rb.velocity.y);
        }

        if (player.horizontalMovement > 0f || player.horizontalMovement < 0f)
        {
            player.SwitchState(PlayerState.MOVING);
        }

        if(Input.GetButton("Jump"))
        {
            player.SwitchState(PlayerState.JUMPING);
        }

        if(Input.GetButton("Crouch"))
        {
            player.SwitchState(PlayerState.CROUCHING);
        }

        if (Input.GetButtonDown("Attack1"))
        {
            Debug.Log("Attacking");
            player.weapon.Attack(0);
        }
        if (Input.GetButtonDown("Attack2"))
        {
            Debug.Log("Attacking");
            player.weapon.Attack(1);
        }
    }
}
