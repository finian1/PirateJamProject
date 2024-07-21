using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    public PlayerControls playerControls;

    public InputActionReference move;

    public bool jumpHold;
    public bool jumpRelease;

    public bool crouchHold;
    public bool crouchRelease;

    public bool fire1Hold;
    public bool fire1Release;

    public bool fire2Hold;
    public bool fire2Release;

    public bool dashHold;
    public bool dashRelease;

    public PlayerStateManager player;

    //Spacebar / Xbox A / Playstation Cross
    public void OnJump(InputAction.CallbackContext context)
    {


        if (context.started)
        {
            player.jumpPress = true;
        }

        jumpHold = context.performed;
        jumpRelease = context.canceled;

    }

    //Left CTRL / Xbox Left Joystick Click / Playstation Left Joystick Click
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.crouchPress = true;
        }

        crouchHold = context.performed;
        crouchRelease = context.canceled;
    }

    //Left Mouse Button / Xbox LT / Playstation L2
    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.fire1Press = true;
        }

        fire1Hold = context.performed;
        fire1Release = context.canceled;
    }

    //Right Mouse Button / Xbox RT / Playstation R2
    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.fire2Press = true;
        }

        fire2Hold = context.performed;
        fire2Release = context.canceled;
    }

    // Shift key / Xbox B / Playstation Circle
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.dashPress = true;
        }

        dashHold = context.performed;
        dashRelease = context.canceled;

    }

}