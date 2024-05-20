using Cinemachine;
using UnityEngine;

public class CamerHandler : MonoBehaviour{

    Transform CameraLookPostion;

    GameObject AimPoint;
    
    [SerializeField] LayerMask AimMask;
    [SerializeField] CinemachineVirtualCamera VirtualCamera;
    [SerializeField] CinemachineBrain CameraBrain;

    float sentivity;
    float Y_Axis, X_Axis;
    void Start(){
        sentivity = 1f;
    }
    private void Awake(){
        CameraLookPostion = GameObject.Find("CameraLookPostion").GetComponent<Transform>();
        AimPoint = GameObject.Find("aimPoint");
    }

    void FixedUpdate(){
        AimCast();
    }

    public void LookAccess(Vector2 input) {

        Y_Axis -= input.y * sentivity;
        X_Axis += input.x * sentivity;

        Y_Axis = Mathf.Clamp(Y_Axis, -40f, 40f);

        CameraLookPostion.localEulerAngles = new Vector3(Y_Axis, CameraLookPostion.localEulerAngles.y, CameraLookPostion.localEulerAngles.z);
        CameraLookPostion.eulerAngles = new Vector3(CameraLookPostion.eulerAngles.x, X_Axis, CameraLookPostion.eulerAngles.z);
    }
    public void AimCast() {
        Vector3 ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        Ray CameraRay = Camera.main.ScreenPointToRay(ScreenCenter);

        RaycastHit HitInfo;

        if(Physics.Raycast(CameraRay,out HitInfo,Mathf.Infinity,AimMask)){
            AimPoint.transform.position = Vector3.Lerp(AimPoint.transform.position,HitInfo.point,10*Time.deltaTime);
            //print(HitInfo.point);
        }
    }
    public GameObject getAimPoint() {
        return AimPoint;
    }

    public float SetFelidOfView{
        set{
            VirtualCamera.m_Lens.FieldOfView = value;
        }
        
    }
    public void CamBrainCntrl(bool value) {
        CameraBrain.enabled = value;
    }
}
