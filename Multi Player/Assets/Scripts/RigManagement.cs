using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigManagement : MonoBehaviour{

    GameObject AImRig;
    Animator RigAnimator;

    GunManagement GunManagementScript;
    AnimationHandler AnimationHandlerScript;

    [Range(0,1)]
    [SerializeField] float weight;

    void Start(){
        
    }
    void Awake(){
        AImRig = GameObject.Find("AImRig");
        RigAnimator = GetComponent<Animator>();
        GunManagementScript = GetComponent<GunManagement>();
        AnimationHandlerScript = GetComponent<AnimationHandler>();
    }


    void Update(){
    }

    public void SetRigWeight(float Weight = 0) {
        AImRig.GetComponent<Rig>().weight = Weight;
    }

    private void OnAnimatorIK(int layerIndex){

        if (GunManagementScript.GetBtnPressValue().Item1 == true || GunManagementScript.GetBtnPressValue().Item2 == true) {
            if (GunManagementScript.GetBtnPressValue().Item3 == false) {
                setGunPostion(GunManagementScript.GetNumberGun);
                SetAndGetWeight = 1;
            }
            else { SetAndGetWeight = 0; }
        }

        else { SetAndGetWeight = 0; }
    }

    void setGunPostion(GameObject GunObj) {
        Transform[] allChildren = GunObj.GetComponentsInChildren<Transform>();
        Transform LeftHand = allChildren.FirstOrDefault(child => child.name == "LeftHand");

        RigAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand.position);
        RigAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);

        RigAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand.rotation);
        RigAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
    }

    public float SetAndGetWeight {
        set { weight = value; }
        get {return  weight; }
    }


    /*
    // -------------------------------------- try to learn something
    void SetGunRig(GameObject GunObj) {


        Rig HandleGunRigObj;
        GameObject LeftHandRigObj;

        AnimationHandler AnimationHandlerScript;

        //HandleGunRigObj = GameObject.Find("HandleGun");
        LeftHandRigObj = HandleGunRigObj.transform.GetChild(0).gameObject;
        AnimationHandlerScript = GetComponent<AnimationHandler>();

        AnimationHandlerScript.OffAnmimator();
        //set gun Position
        TwoBoneIKConstraint Constrain = LeftHandRigObj.GetComponent<TwoBoneIKConstraint>();
        Constrain.data.target = GunObj.transform.GetChild(0).transform;
        Constrain.data.hint = GunObj.transform.GetChild(1).transform;
        Constrain.weight = 1f;

        //set hand rotation;
        MultiRotationConstraint rotateConstrain =  LeftHandRigObj.GetComponent<MultiRotationConstraint>();
        rotateConstrain.data.sourceObjects.SetTransform(0, GunObj.transform.GetChild(0).transform);
        AnimationHandlerScript.OnAnmimator();
    }*/
}
