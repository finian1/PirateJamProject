using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string dash = "Dash";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction dashAction;


    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool CrouchTriggered { get; private set; }
    public bool DashTriggered { get; private set; }


    public static PlayerInputSystem Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        crouchAction = playerControls.FindActionMap(actionMapName).FindAction(crouch);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        crouchAction.performed += context => CrouchTriggered = true;
        crouchAction.canceled += context => CrouchTriggered = false;

        dashAction.performed += context => DashTriggered = true;
        dashAction.canceled += context => DashTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        crouchAction.Enable();
        dashAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        crouchAction.Disable();
        dashAction.Disable();
    }

    ////public PlayerControls playerControls;

    //public InputActionReference move;

    //public bool jumpHold;
    //public bool jumpRelease;

    //public bool crouchHold;
    //public bool crouchRelease;

    //public bool fire1Hold;
    //public bool fire1Release;

    //public bool fire2Hold;
    //public bool fire2Release;

    //public bool dashHold;
    //public bool dashRelease;

    //public PlayerStateManager player;

    ////private void OnEnable()
    ////{
    ////    playerControls.Enable();
    ////}

    ////private void OnDisable()
    ////{
    ////    playerControls.Disable();
    ////}

    ////Spacebar / Xbox A / Playstation Cross
    //public void OnJump(InputAction.CallbackContext context)
    //{


    //    if (context.started)
    //    {
    //        player.jumpPress = true;
    //    }

    //    jumpHold = context.performed;
    //    jumpRelease = context.canceled;

    //}

    ////Left CTRL / Xbox Left Joystick Click / Playstation Left Joystick Click
    //public void OnCrouch(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        player.crouchPress = true;
    //    }

    //    crouchHold = context.performed;
    //    crouchRelease = context.canceled;
    //}

    ////Left Mouse Button / Xbox LT / Playstation L2
    //public void OnAttack1(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        player.fire1Press = true;
    //    }

    //    fire1Hold = context.performed;
    //    fire1Release = context.canceled;
    //}

    ////Right Mouse Button / Xbox RT / Playstation R2
    //public void OnAttack2(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        player.fire2Press = true;
    //    }

    //    fire2Hold = context.performed;
    //    fire2Release = context.canceled;
    //}

    //// Shift key / Xbox B / Playstation Circle
    //public void OnDash(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        player.dashPress = true;
    //    }

    //    dashHold = context.performed;
    //    dashRelease = context.canceled;

    //}

}