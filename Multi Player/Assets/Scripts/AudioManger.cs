using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour{

    [SerializeField] AudioSource PlayerRun;
    [SerializeField] AudioSource Shote;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void PlayRun() {
        PlayerRun.Play();
    }
    public void StopRun(){
        PlayerRun.Stop();
    }
    public void Fire(){
        Shote.Play();
    }
}
