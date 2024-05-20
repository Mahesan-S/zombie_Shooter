using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CutScenceManager : MonoBehaviour
{
    [SerializeField] PlayableDirector ZombieEntryClip;
    void Start(){
        
    }

    void Update()
    {
        
    }
    public IEnumerator ZomblieEntryClip() { 
        ZombieEntryClip.Play();
        yield return null;
        /*Time.timeScale = 0;
        yield return new WaitForSeconds(24f);
        Time.timeScale = 1f;
        */
    }
}
