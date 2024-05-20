using UnityEngine;

public class CrusorHandler : MonoBehaviour{

    GunManagement GunManagementScript;

    [SerializeField] Texture2D CursorTexture,
                               NormalCursor;
    [SerializeField] GameObject TragetPoint;

    void Awake(){
        GunManagementScript = GetComponent<GunManagement>(); 
    }
    void Start(){
        SetCursorTexture();
        TragetPoint.SetActive(false);
    }

    void Update(){
        Cursor.visible = true;
        SetTarget();
    }

    void SetCursorTexture(){
        Vector2 hostpost = new Vector2(CursorTexture.width/2, CursorTexture.height/2);
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(NormalCursor, hostpost, CursorMode.Auto);

    }
    void SetTarget() { 
        if(GunManagementScript.GetBtnPressValue().Item1 == true || GunManagementScript.GetBtnPressValue().Item2 == true) {
            TragetPoint.SetActive(true);
        }
        else {
            TragetPoint.SetActive(false);
        }
        
    }
}
