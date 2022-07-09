using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeSound : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;
   
    [SerializeField]
    private AudioClip [] axe_Sounds;

    void PlayAxeSound() {
        audioSource.clip = axe_Sounds[Random.Range(0, axe_Sounds.Length)];
        audioSource.Play();
    }

} // class
