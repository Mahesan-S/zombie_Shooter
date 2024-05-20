using UnityEngine;

public class Bullet : MonoBehaviour{


    private void OnTriggerEnter(Collider collision){
        if (collision.gameObject.CompareTag("EZ")){
            CallZombieFuntion(collision.gameObject);


        }
        else if (collision.gameObject.CompareTag("MZ")){
            CallZombieFuntion(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("HZ")){
            CallZombieFuntion(collision.gameObject);

        }
        Destroy(this.gameObject);
    }

    void CallZombieFuntion(GameObject obj) {
        obj.GetComponent<Zombies>().ReduceLife(obj.tag);
    }

}
