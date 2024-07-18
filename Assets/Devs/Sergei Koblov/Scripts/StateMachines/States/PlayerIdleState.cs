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
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (player.horizontalMovement > 0f || player.horizontalMovement < 0f)
        {
            player.SwitchState(player.MovingState);
        }

        if(Input.GetButton("Jump"))
        {
            player.SwitchState(player.JumpingState);
        }

        if(Input.GetButton("Crouch"))
        {
            player.SwitchState(player.CrouchingState);
        }
    }
}
