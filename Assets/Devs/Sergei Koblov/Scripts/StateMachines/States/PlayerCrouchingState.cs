using UnityEngine;

public class PlayerCrouchingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is crouching.");
        player.hasCrouchFlipReset = false;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.horizontalMovement = Input.GetAxisRaw("Horizontal");

        player.currentMovementSpeed = 0f;

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

        if (!Input.GetButton("Crouch"))
        {
            player.SwitchState(PlayerState.IDLE);
        }

        if(Input.GetButton("Crouch") && player.horizontalMovement != 0f)
        {
            player.SwitchState(PlayerState.CROUCHMOVING);
        }
    }
}
