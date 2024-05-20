using UnityEngine;

public class AllObjTriggerManager : MonoBehaviour{

    void Start(){
        
    }

    void Update(){

    }
    public void OnCollisionEnter(Collision collision){
        print(collision.gameObject.name);
    }
}
