using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public float damage = 2f;
    public float radius = 1f;
    public LayerMask enemy_LayerMask;

    void Update() {

        // Physics.OverlapSphere will detect an object and returns a collider array
        // it will only detect collision with gameobjects who are on the layerMask
        // radius is how large the sphere is
        // so basically it will put whatever gameobject, inside the Colider[] hits
        Collider [] hits = Physics.OverlapSphere(transform.position, radius, enemy_LayerMask);
        if (hits.Length > 0) {
            print("we touched" + hits[0].gameObject.tag);

            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
        
    }
}
