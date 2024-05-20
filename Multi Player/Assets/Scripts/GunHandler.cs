using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour{
    AnimationHandler AnimationHandlerScripts;
    CanvasHandler CanvasHandlerScript;
    PlayerMovement PlayerMovementScript;
    GunManagement GunManagementScript;

    [SerializeField] GameObject Button;
    [SerializeField] GameObject GunContainerUi;
    [SerializeField] GameObject HideBtn;
    GameObject ButtonPrefab;


    void Start(){
        if (GunContainerUi == null) { GunContainerUi = GameObject.Find("ItemShowContainer"); }
        if (Button == null) { Button = Resources.Load<GameObject>("Button"); }


    }
    void Awake(){
        AnimationHandlerScripts = GameObject.FindWithTag("Player").GetComponent<AnimationHandler>();
        PlayerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        GunManagementScript = GameObject.FindWithTag("Player").GetComponent<GunManagement>();
        CanvasHandlerScript = GameObject.Find("Canvas").GetComponent<CanvasHandler>();
    }

    public void GunPickUp() {
        if(GunManagementScript.CheckHowManyGun < 2) {
            Destroy(ButtonPrefab);
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(AnimationHandlerScripts.PickUpGun()); 
            CanvasHandlerScript.ReduceitemShowPanelSize(HideBtn);
            GunManagementScript.AddGunObj(this.gameObject);
        }
        else {
            print("gun is full"+ GunManagementScript.CheckHowManyGun);
        }
    }


    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")) {
            ButtonPrefab = Instantiate(Button, GunContainerUi.transform);
            
            ButtonPrefab.name = this.gameObject.tag;
            TMP_Text ButttonTest = ButtonPrefab.GetComponentInChildren<TMP_Text>();
            ButttonTest.text = ButtonPrefab.gameObject.name;
            
            CanvasHandlerScript.AdditemShowPanelSize();
            SetGunPickUPButtonEvent(ButtonPrefab);
            HideBtn.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Player")) {
            CanvasHandlerScript.ReduceitemShowPanelSize(HideBtn);
            HideBtn.SetActive(false);
            Destroy(ButtonPrefab);

        }    
    }
    public void SetGunPickUPButtonEvent(GameObject ButtonObj) {
        Button btn = ButtonObj.GetComponent<Button>();
        btn.onClick.AddListener(GunPickUp);
    }
}
