using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {
    
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_CLip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    [SerializeField]
    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    void Awake() {

        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
    }

    void Update() {

        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound() {

        // if we are NOT on the ground
        if(!character_Controller.isGrounded) 
            return; // if return then code below wont execute and nothing happens
        
        if(character_Controller.velocity.sqrMagnitude > 0 ) {

            // accumulated distance is the value how far we can go
            // e.g make a step or sprint, or move while crouching
            // until we play the footstep sound
            // how much are we allowing the accumalted distance to go, to the step distance, before we play the sound

            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance > step_Distance) {

                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_CLip[Random.Range(0, footstep_CLip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        } 
        else {

            accumulated_Distance = 0f;
        }
    }
} // class
