using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour{

    Animator PlayerAnimator, Pause_Resume;

    Animation LiverAnimation,GateOpenAnimation;

    GunManagement GunManagementScript;
    AccessVariable AccessVariableScript;


    int GunLayer;
    void Start(){
        GunLayer = PlayerAnimator.GetLayerIndex("Gun Layer");
    }
    void Awake(){
        PlayerAnimator = GetComponent<Animator>();
        GunManagementScript = GetComponent<GunManagement>(); 
        LiverAnimation = GameObject.Find("GateOpenLiver").GetComponent<Animation>();
        GateOpenAnimation = GameObject.Find("gate").GetComponent<Animation>();
        Pause_Resume = GameObject.Find("PausePanle").GetComponent<Animator>();

        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();

    }

    void Update(){
        
    }
    public void PlayerCheckStates(bool PlayerMoving = false, bool PlayerWalking = false, bool Die = false ) {

        if (Die) {
            StartCoroutine(PlayerDie());
        }
        else if (PlayerMoving){
            PlayerAnimator.SetInteger("States", 1);
        }
        else {
            PlayerAnimator.SetInteger("States", 0);
        }
        
    }
    public IEnumerator PickUpGun(string Value = "PickUp") {
        PlayerAnimator.SetBool(Value, true);
        yield return new WaitForSeconds(0.5f);
        PlayerAnimator.SetBool(Value, false);
    }

    public IEnumerator PlayerDie() {
        PlayerAnimator.SetInteger("States", 6);
        GetComponent<CharacterController>().Move(Vector3.zero);
        GetComponent<CharacterController>().enableOverlapRecovery = false;
        yield return new WaitForSeconds(2f);
        AccessVariableScript.CamerHandlerScript.CamBrainCntrl(false);
        PauseAndResume(1);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
    // ------------------------------------------------- layers
    public void GunPostionWeight(float weight = 0) {
        PlayerAnimator.SetLayerWeight(GunLayer, weight);
    }
    // ----------------------------------------------  gun layer function
    public void FireStates(int StatesValue) {
        PlayerAnimator.SetInteger("FireStates", StatesValue);
    }
    public void Aim(bool value) {
        PlayerAnimator.SetBool("Aim", value);
    }

    public IEnumerator ReloadAmmo() {
        GunManagementScript.ReloadValue = true;
        FireStates(2);
        yield return new WaitForSeconds(2f);
        FireStates(0);
        GunManagementScript.ReloadValue = false;
    }


    //---------------------------------- on off function
    public void OnAnmimator(){
        PlayerAnimator.enabled = true;
    }
    public void OffAnmimator(){
        PlayerAnimator.enabled = true;
    }

    //-------------------------------------------- zombile 1 Animaiton Handler

    public void ZombieStates(int StatesValue,GameObject obj) {
        Animator tempAnimator = obj.GetComponent<Animator>();
        string ParameterName = tempAnimator.GetParameter(0).name;
        tempAnimator.SetInteger(ParameterName, StatesValue);
    } 
    //------------------------------------------ all gate object Animation

    public IEnumerator Liver() {
        PlayerAnimator.SetBool("Pull", true);
        LiverAnimation.Play();
        yield return new WaitForSeconds(3f);
        GateOpenAnimation.Play();
        PlayerAnimator.SetBool("Pull", false);
    }
    public void PauseAndResume(int value) {
        Pause_Resume.SetInteger("MenuState", value);
    }
}
