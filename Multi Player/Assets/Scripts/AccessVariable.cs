using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessVariable : MonoBehaviour {

    //------------ player
    public bool LiverAccess { set;get;}
    [SerializeField] Slider PlayerLife;

    public float Playerlife{
        set { 
            if(PlayerLife.value >= 0) { PlayerLife.value += value; }
        }
        get { 
            return PlayerLife.value; 
        }
    }

   //--------------------- zombies access  start
    public bool gateOpen { set;get; }
    public int ZombieCount { set;get; }

    public AnimationHandler AnimationHandlerScript;
    //--------------------------------------------- zombies access end

    //--------------------------------- bullet Start
    [SerializeField] public float Timer { set; get; }
    public bool shoot { set; get; }
    //--------------------------------- bullet end

    //================================ canvas
    public bool GameisPaues { set; get; }

    //============================ Audio
    public AudioManger AudioManagerScript;
    public CamerHandler CamerHandlerScript;
    private void Awake(){
        AnimationHandlerScript = GameObject.FindWithTag("Player").GetComponent<AnimationHandler>();
    }
    void Start(){
    }
    private void Update(){
        Timer -= Time.deltaTime;
    }
}
