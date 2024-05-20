using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunManagement : MonoBehaviour{

    AnimationHandler AnimationHandlerScript;
    RigManagement RigManagementScript;
    CamerHandler CamerHandlerScript;
    AccessVariable AccessVariableScript;

    GameObject[] Gunlist = new GameObject[2];
    [SerializeField] GameObject[] gunPostionList;
    [SerializeField] Button[] GunButtonsList;
    
    GameObject[] first_Aid = new GameObject[3];
    int[] AmmoList = new int[2];

    [SerializeField] GameObject gunPostion;

    GameObject BulletPrefab;

    int index,
        GunNumber;

    bool Btn_1IsPress,
         Btn_2IsPress,
         reload;


    void Start(){
        index = 0;
        GunNumber = -1;

        Btn_1IsPress = false;
        Btn_2IsPress = false;
        reload = false;

        AmmoList[0] = AmmoList[1] = 30;
    }
    void Awake(){
        BulletPrefab = Resources.Load<GameObject>("AR_Bullet");
        AnimationHandlerScript = GetComponent<AnimationHandler>();
        RigManagementScript = GameObject.FindWithTag("Player").GetComponent<RigManagement>();
        CamerHandlerScript = GameObject.FindWithTag("MainCamera").GetComponent<CamerHandler>();
        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();
    }

    void Update(){
        //print(GunButtonsList[0].interactable);
    }

    public void AddGunObj(GameObject gunObj) { 

        Gunlist[index] = gunObj;
        setGunPostion(Gunlist[index],index);
        showGunInCanvas(Gunlist[index],index);
        index++;
       

    }

    public int CheckHowManyGun {
        get { return index; }
    }

    public void setGunPostion(GameObject gunobj,int index) {
        gunobj.transform.SetParent(gunPostionList[index].transform);
        gunobj.transform.SetPositionAndRotation(gunPostionList[index].transform.position,
                                                 gunPostionList[index].transform.rotation);

    }

    public void showGunInCanvas(GameObject gunObj, int index) { 

        TMP_Text btnText = GunButtonsList[index].GetComponentInChildren<TMP_Text>();
        btnText.text = gunObj.tag;

        GunButtonsList[index].transform.GetChild(1).GetComponent<TMP_Text>().text = $"{AmmoList[index]} / unlimt";
    }

    public void GunInHandBtn_1() {
        if (Gunlist[0] != null) {
            
            if(Btn_1IsPress == false  && Btn_2IsPress == false) {
                GunNumber = 0;
                setGunHand(Gunlist[GunNumber]);
                Btn_1IsPress = true;
            }
            else if(Btn_1IsPress == false && Btn_2IsPress == true) {
                Btn_2IsPress = false;
                setGunPostion(Gunlist[1], 1);
                Btn_1IsPress = true;
                GunNumber = 0;
                setGunHand(Gunlist[GunNumber]);
            }
            else {
                setGunPostion(Gunlist[0], 0);
                Btn_1IsPress = false;
                AnimationHandlerScript.GunPostionWeight();
                GunNumber = -1;
            }
        }
    }

    public void GunInHandBtn_2(){
        if (Gunlist[1] != null){
            
            if(Btn_1IsPress == false && Btn_2IsPress == false) {
                Btn_2IsPress = true;
                GunNumber = 1;
                setGunHand(Gunlist[GunNumber]);
            }
            else if (Btn_1IsPress == true && Btn_2IsPress == false){
                Btn_1IsPress = false;
                setGunPostion(Gunlist[0], 0);
                Btn_2IsPress = true;
                GunNumber = 1;
                setGunHand(Gunlist[GunNumber]);
            }
            else {
                Btn_2IsPress = false;
                setGunPostion(Gunlist[1], 1);
                AnimationHandlerScript.GunPostionWeight();
                GunNumber = -1;
            }
        }
    }

    void setGunHand(GameObject obj) {

        obj.transform.SetParent(gunPostion.transform);
        obj.transform.SetPositionAndRotation(gunPostion.transform.position,
                                                        gunPostion.transform.rotation);
        AnimationHandlerScript.GunPostionWeight(1f);
    }

    public void AimAcess(bool Aimvalue) {
        if (GunNumber >= 0) {
            if(Aimvalue) {
                CamerHandlerScript.SetFelidOfView = 30;
                AnimationHandlerScript.Aim(Aimvalue);
                RigManagementScript.SetRigWeight(1f);
            }
            else{
                CamerHandlerScript.SetFelidOfView = 40f;
                AnimationHandlerScript.Aim(Aimvalue);
                RigManagementScript.SetRigWeight(0);
            }
        }
        else {
            //no gun
        }
    }

    public void FireAccess(bool value) {
        if (GunNumber >= 0) {
            if(value) {
                AnimationHandlerScript.FireStates(1);
                RigManagementScript.SetRigWeight(1f);
                CamerHandlerScript.SetFelidOfView = 30;
            }
            else {
                AnimationHandlerScript.FireStates(0);
                RigManagementScript.SetRigWeight();
                CamerHandlerScript.SetFelidOfView = 40f;
                //Gunlist[GunNumber].transform.LookAt(this.gameObject.transform);
            }
        }
        else { 
            //no gun
        }
    }
    public void fire() {
        //Gunlist[GunNumber].transform.LookAt(CamerHandlerScript.getAimPoint().transform);
        Transform position = Gunlist[GunNumber].transform.GetChild(0);
        position.LookAt(CamerHandlerScript.getAimPoint().transform);
        GameObject BulletObj = Instantiate(BulletPrefab, position.position, position.rotation);
        BulletObj.GetComponent<Rigidbody>().AddForce(position.forward * 100,ForceMode.Impulse);
        AccessVariableScript.AudioManagerScript.Fire();
        CountAmmo();
        
    }

    public void CountAmmo() {
        if (AmmoList[GunNumber] > 0){
            GunButtonsList[GunNumber].transform.GetChild(1).GetComponent<TMP_Text>().text = $"{--AmmoList[GunNumber]} / unlimt";
        }
        else {
            StartCoroutine(AnimationHandlerScript.ReloadAmmo());
            AmmoList[GunNumber] = 30;
        }
         
    }

    public Tuple<bool, bool,bool> GetBtnPressValue(){
        return Tuple.Create(Btn_1IsPress, Btn_2IsPress,reload);
    }

    public GameObject GetNumberGun {
        get { return Gunlist[GunNumber]; }
    }

    public bool ReloadValue {
        set { reload = value; }
        get { return reload; }
    }
}
