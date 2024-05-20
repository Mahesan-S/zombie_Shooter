using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesSpawnManager : MonoBehaviour{

    AccessVariable AccessVariableScript;

    BoxCollider ZombileSpawnBoxColider;
    List<GameObject> ZombieList = new List<GameObject>();

    [SerializeField] float countDown = 10;
    void Start(){
    }
    void Awake(){
        ZombileSpawnBoxColider = GetComponent<BoxCollider>();
        ZombieList.Add(Resources.Load<GameObject>("Easy Zombie"));
        ZombieList.Add(Resources.Load<GameObject>("medium Zombie"));
        ZombieList.Add(Resources.Load<GameObject>("Hard Zombie"));

        AccessVariableScript = GameObject.Find("All Access Variable").GetComponent<AccessVariable>();
    }


    void Update(){
        countDown -= Time.deltaTime;
        zombieSpawn();
    }
    void zombieSpawn() {

        if (AccessVariableScript.gateOpen ) {
            if (countDown > 0 && AccessVariableScript.ZombieCount < 150) { 
                float X_Axis = Random.Range(ZombileSpawnBoxColider.bounds.min.x, ZombileSpawnBoxColider.bounds.max.x);
                float Z_Axis = Random.Range(ZombileSpawnBoxColider.bounds.min.z, ZombileSpawnBoxColider.bounds.max.z);

                Vector3 position = new Vector3(X_Axis, this.transform.position.y, Z_Axis);

                int zombieSelect = Random.Range(0, ZombieList.Count);

                Instantiate(ZombieList[0], position, Quaternion.identity);
                AccessVariableScript.ZombieCount++;
            }
            else { countDown = 10; AccessVariableScript.ZombieCount = 0; }
        }
    }
}
