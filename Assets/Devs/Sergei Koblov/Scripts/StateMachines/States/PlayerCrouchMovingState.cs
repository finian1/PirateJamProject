using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouchMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is crouch moving.");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (player.horizontalMovement != 0)
        {
            player.currentMovementSpeed = player.crouchingMovementSpeed;
            player.rb.velocity = new Vector2(player.horizontalMovement * player.currentMovementSpeed, player.rb.velocity.y);
        }

        float heightDifference = player.currentScale.y - player.crouchScale.y;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - heightDifference * 0.8f, player.transform.position.z);
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

        if (player.isFacingRight && player.horizontalMovement < 0f || !player.isFacingRight && player.horizontalMovement > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.currentScale;
            localScale.x *= -1f;
            player.currentScale = localScale;
        }

        if (!Input.GetButton("Crouch") && player.horizontalMovement == 0)
        {
            player.SwitchState(player.IdleState);
        }

        if(!Input.GetButton("Crouch") && player.horizontalMovement != 0)
        {
            player.SwitchState(player.MovingState);
        }

        if(Input.GetButton("Crouch") && player.horizontalMovement == 0)
        {
            player.SwitchState(player.CrouchingState);
        }
    }
}
