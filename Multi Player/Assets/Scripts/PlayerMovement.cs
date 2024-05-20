using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour{

    CharacterController PlayerCharacterController;

    AnimationHandler AnimationHandlerScript;
    CanvasHandler CanvasHandlerScript;
    AccessVariable AccessVariableScript;
    CutScenceManager CutScenceManagerScript;

    [SerializeField] Transform rayCastTranform;
    
    Vector3 playerCurrentPosition;

    float PlayerRunSpeed, PlayerWalkSpeed,turnSmoothVelocity;
    bool PlayerIsWalk,PlayerIsMoveing;
    
    [SerializeField] bool Intraction;

    void Start(){
        PlayerWalkSpeed = 2f;
        PlayerRunSpeed = 3f;
    }
    private void Awake(){
        PlayerCharacterController = GetComponent<CharacterController>();
        AnimationHandlerScript = GetComponent<AnimationHandler>();

        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();
        CutScenceManagerScript = GameObject.Find("CutScence").GetComponent<CutScenceManager>();
        CanvasHandlerScript = GameObject.Find("Canvas").GetComponent<CanvasHandler>();
    }

    void FixedUpdate(){
        MovePlayer();
        HandleGarvity();
        findObjRayCast();
        RotationHandler();
        //RotationPLayer();
    }
    void Update(){
        if (AccessVariableScript.Playerlife > 0f) { AnimationHandlerScript.PlayerCheckStates(PlayerIsMoveing, PlayerIsWalk); }
        if (AccessVariableScript.Playerlife == 0f) { StartCoroutine(AnimationHandlerScript.PlayerDie()); }
        

    }
    void MovePlayer(){
      
       /* if (playerCurrentPosition.magnitude >= 0.1f)
        {
            float TragetAngle = Mathf.Atan2(playerCurrentPosition.x, playerCurrentPosition.z) * Mathf.Rad2Deg * cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TragetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, TragetAngle, 0f) * Vector3.forward;
            PlayerCharacterController.Move(moveDir.normalized * PlayerWalkSpeed * Time.deltaTime);

        }*/
        if (PlayerIsWalk){
            PlayerCharacterController.Move(playerCurrentPosition * PlayerWalkSpeed * Time.deltaTime);
            
        }
        else{
            PlayerCharacterController.Move(playerCurrentPosition * PlayerRunSpeed * Time.deltaTime);
        }

    }
    void RotationHandler(){
        float PlayerRotatonFrame = 15f;
        Vector3 LookAtPosition;
        LookAtPosition.x = playerCurrentPosition.x;
        LookAtPosition.y = 0f;
        LookAtPosition.z = playerCurrentPosition.z;

        Quaternion currentPosition = transform.rotation;
        if (PlayerIsMoveing){
            Quaternion targetPosition = Quaternion.LookRotation(LookAtPosition);
            transform.rotation = Quaternion.Slerp(currentPosition, targetPosition, PlayerRotatonFrame * Time.deltaTime);
        }
    }
    void HandleGarvity() {
        if (PlayerCharacterController.isGrounded) {
            playerCurrentPosition.y = -1f;
        }
        else {
            playerCurrentPosition.y += -9.8f;
        }
    
    }
    void findObjRayCast() {
        RaycastHit HitInfo;
        Vector3[] directions ={
            Vector3.forward ,
            Vector3.forward *2 + Vector3.up,
            Vector3.forward *2 + Vector3.down,

        };
        foreach (Vector3 direction in directions){
            if (Physics.Raycast(rayCastTranform.position, direction, out HitInfo, 3f)){
                if (HitInfo.collider.gameObject.name == "GateOpenLiver"){
                    LiverFunction();
                }
                else { CanvasHandlerScript.showmessage(""); }
            }
            
        }
    }

    void LiverFunction() {
        if (!AccessVariableScript.LiverAccess) {
            CanvasHandlerScript.showmessage();
            if (Intraction){
                StartCoroutine(AnimationHandlerScript.Liver());
                AccessVariableScript.LiverAccess = true;
                StartCoroutine(CutScenceManagerScript.ZomblieEntryClip());
                AccessVariableScript.gateOpen = true;
            }
        }
        
    }
    //------------------------------------------input access felid
    public void MoveAcess(Vector2 input){
        playerCurrentPosition.z = input.y;
        playerCurrentPosition.x = input.x;

        playerCurrentPosition = playerCurrentPosition.normalized;
        //playerCurrentPosition = transform.TransformDirection(playerCurrentPosition);

        if (input.x != 0 || input.y != 0) { PlayerIsMoveing = true; AccessVariableScript.AudioManagerScript.PlayRun(); }
        else{ PlayerIsMoveing = false; AccessVariableScript.AudioManagerScript.StopRun(); }
    }
    public void WalkAcess(bool value) {
        PlayerIsWalk = value;
    }

    public void JumpAcess(bool value) {
        //PlayerIsJumping = value;
    }

    public void IntractionValue(bool value) {
         Intraction = value;
    }
    //-------------------------------------------- animation send messgae

  public bool PlayerIsRunning {
        get { 
            return PlayerIsMoveing;
        }
    }
}
