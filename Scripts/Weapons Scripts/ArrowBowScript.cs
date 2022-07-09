using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour { 
    
    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivate_Timer = 3f;

    public float damage = 15f;

    void Awake() {

        myBody = GetComponent<Rigidbody>();
    }

    void Start() {
        Invoke("Deactivate_GameObject", deactivate_Timer);

    }

    void Deactivate_GameObject() {

        if (gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
            
        }
    }

    public void Launch(Camera mainCamera) {

        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }

    void OnTriggerEnter(Collider target) {
        //after we touch an enemy deactivate gameobject
        if (target.tag == Tags.ENEMY_TAG) {
            target.GetComponent<HealthScript>().ApplyDamage(damage);

            // gameObject.SetActive(false);
        }
    }

} // class

