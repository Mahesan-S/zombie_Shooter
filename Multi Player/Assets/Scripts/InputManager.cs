using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{

    PlayerMovement PlayerMovementScript;
    CamerHandler CamerHandlerScripts;
    GunManagement GunManagementScript;

    PlayerCntrl PlayerInput;
   
    void Start(){
        PlayerMovementScript = GetComponent<PlayerMovement>();
        GunManagementScript = GetComponent<GunManagement>();
        CamerHandlerScripts = GameObject.FindWithTag("MainCamera").GetComponent<CamerHandler>();
    }
    private void Awake(){
        PlayerInput = new PlayerCntrl();
       AccessAllPlayerInput();
        
    }

    void Update(){
        
    }
    public void OnMove(InputAction.CallbackContext  input){
        Vector2 MoveMent = input.ReadValue<Vector2>();
        PlayerMovementScript.MoveAcess(MoveMent);
    }

    public void OnLook(InputAction.CallbackContext input) {
        Vector2 LookMove = input.ReadValue<Vector2>();
        //PlayerMovementScript.LookAcess(LookMove);
        CamerHandlerScripts.LookAccess(LookMove);

    }
    public void OnWalk(InputAction.CallbackContext context) {
        PlayerMovementScript.WalkAcess(context.ReadValueAsButton());
    }
    private void OnJump(InputAction.CallbackContext context){
        PlayerMovementScript.JumpAcess(context.ReadValueAsButton());
    }

    void OnFire(InputAction.CallbackContext context) {
        GunManagementScript.FireAccess(context.ReadValueAsButton());
    }
    void OnAim(InputAction.CallbackContext context) {
        GunManagementScript.AimAcess(context.ReadValueAsButton());
    }
    void Onintraction(InputAction.CallbackContext context) {
        PlayerMovementScript.IntractionValue(context.ReadValueAsButton());
    }
    void OnTakeGun(InputAction.CallbackContext context) {
       if(context.control.displayName == "E") { GunManagementScript.GunInHandBtn_2(); }
       if(context.control.displayName == "Q") { GunManagementScript.GunInHandBtn_1(); }    
    }
    void AccessAllPlayerInput() {
        PlayerInput.Player.Walk.started += OnWalk;
        PlayerInput.Player.Walk.canceled += OnWalk;

        PlayerInput.Player.Move.started += OnMove;
        PlayerInput.Player.Move.canceled += OnMove;
        PlayerInput.Player.Move.performed += OnMove;

        PlayerInput.Player.Look.started += OnLook;
        PlayerInput.Player.Look.canceled += OnLook;

        PlayerInput.Player.Jump.started += OnJump;
        PlayerInput.Player.Jump.canceled += OnJump;

        PlayerInput.Player.Fire.started += OnFire;
        //PlayerInput.Player.Fire.performed += OnFire;
        PlayerInput.Player.Fire.canceled += OnFire;

        PlayerInput.Player.Aim.started += OnAim;
        PlayerInput.Player.Aim.canceled += OnAim;

        PlayerInput.Player.intraction.started += Onintraction;
        PlayerInput.Player.intraction.canceled += Onintraction;

        PlayerInput.Player.TakeGun.started += OnTakeGun;
        //PlayerInput.Player.TakeGun.canceled += OnTakeGun;

    }



    public void OnEnable(){
        PlayerInput.Player.Enable();
    }
    public void OnDisable(){
        PlayerInput.Player.Disable();
    }
}
