using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    public PlayerControls playerControls;

    public InputActionReference move;

    public bool jumpButton;
    public bool crouchButton;
    public bool fireButton1;
    public bool fireButton2;


    //Spacebar
    public void OnJump(InputAction.CallbackContext context)
    {
        jumpButton = context.performed;
    }

    //Left CTRL
    public void OnCrouch(InputAction.CallbackContext context)
    {
        crouchButton = context.performed;
    }

    //Left Mouse Button
    public void OnAttack1(InputAction.CallbackContext context)
    {
        fireButton1 = context.performed;
    }

    //Right Mouse Button
    public void OnAttack2(InputAction.CallbackContext context)
    {
        fireButton2 = context.performed;
    }

}