using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombies : MonoBehaviour{

    NavMeshAgent ZombieNav;
    Transform PlayerTranform;
    AccessVariable AccessVariableScript;

    [SerializeField]
    AudioSource ZombieScreme,
                ZombieBite;

    float EZ_Life, MZ_Life, HZ_Life;
    bool die;

    public float distance,dot;

    void Start(){
        EZ_Life = 20;
        MZ_Life = 35;
        HZ_Life = 50;
        die = false;

    }
    void Awake(){
        ZombieNav = GetComponent<NavMeshAgent>();
        PlayerTranform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();

    }

    void Update(){

        if(ZombieNav.isStopped  == false) {
            ZombieNav.destination = PlayerTranform.transform.position;
        }
        
        transform.LookAt(PlayerTranform.transform.position);
        
        if (!die) { UpdateZombieAnimation(); }

        if (Input.GetKeyDown(KeyCode.M)) { ZombieNav.isStopped = true; }

        Vector3 Direction = transform.position - PlayerTranform.position;

        dot = Vector3.Dot(Direction, transform.forward); 
        
    }
    public void ReduceLife(string zombieName,float value = 5) {
        if (gameObject.tag == zombieName) {
            EZ_Life -= value;
            if (EZ_Life <= 0) { StartCoroutine(Die()); }
        }

        else if (gameObject.tag == zombieName){
            MZ_Life -= value;
            if (MZ_Life <= 0) { StartCoroutine(Die()); }
        }

        else if (gameObject.tag == zombieName){
            HZ_Life -= value;
            if (HZ_Life <= 0) { StartCoroutine(Die()); }
        }
        else { return; }

    }
    void UpdateZombieAnimation() {
        distance = Vector3.Distance(this.gameObject.transform.position, PlayerTranform.position);
        if (Vector3.Distance(this.gameObject.transform.position, PlayerTranform.position) <= 1f){
            AccessVariableScript.AnimationHandlerScript.ZombieStates(2, this.gameObject);
            BiteThePlayer();
            ZombieBite.Play();
        }
        else if (Vector3.Distance(this.gameObject.transform.position, PlayerTranform.position) <= 15f &&
                 Vector3.Distance(this.gameObject.transform.position, PlayerTranform.position) >= 10f)  {

            StartCoroutine(Screm());
        }
        else {
            AccessVariableScript.AnimationHandlerScript.ZombieStates(0, this.gameObject);
        }
        
            
       
    }
    void BiteThePlayer() { 
        if(this.gameObject.tag == "EZ") { AccessVariableScript.Playerlife = -0.3f; }
        if(this.gameObject.tag == "MZ") { AccessVariableScript.Playerlife = -2; }
        if(this.gameObject.tag == "HZ") { AccessVariableScript.Playerlife = 5; }
    }
    IEnumerator Die() {
        die = true;
        ZombieNav.isStopped = true;
        AccessVariableScript.AnimationHandlerScript.ZombieStates(3, this.gameObject);
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    IEnumerator Screm() {
        AccessVariableScript.AnimationHandlerScript.ZombieStates(1, this.gameObject);
        ZombieNav.isStopped = true;
        ZombieBite.Stop();
        ZombieScreme.Play();
        print("start 3");
        yield return new WaitForSeconds(3f);
        print("over 3");
        AccessVariableScript.AnimationHandlerScript.ZombieStates(0, this.gameObject);
        ZombieNav.isStopped = false;
        ZombieBite.Play();
    }
}
