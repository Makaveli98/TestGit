using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {

    private PlayerMovement player_Movement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private Transform look_Rotation;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    [SerializeField]
    private bool is_Crouching;

    [SerializeField]
    private bool is_Sprinting = false;

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;

    private PlayerStats player_Stats;
    
    private float sprint_Value = 100f;
    private float sprint_Treshold = 10f;

    private CharacterController character_Controller;

    void Awake() {

        player_Movement = GetComponent<PlayerMovement>();

        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();

        look_Rotation = transform.GetChild(0);

        player_Stats = GetComponent<PlayerStats>();

        character_Controller = GetComponent<CharacterController>();

        
    }

    void Start() {
        
        player_Footsteps.step_Distance = walk_Step_Distance;
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
    }

    void Update() {
        Sprint();
        Crouch();
    }

    void Sprint() {
        
        // if we have stamina we can sprint
        if(sprint_Value > 0f) { 

            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching 
            && character_Controller.velocity.sqrMagnitude > 0) {

            player_Movement.speed = sprint_Speed;

            player_Footsteps.step_Distance = sprint_Step_Distance;
            player_Footsteps.volume_Min = sprint_Volume;
            player_Footsteps.volume_Max = sprint_Volume;

            }

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) {
            
            player_Movement.speed = move_Speed;

            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;

        }


        // this is for the stamina
        if (Input.GetKey(KeyCode.LeftShift) && !is_Crouching) {

            sprint_Value -= sprint_Treshold * Time.deltaTime;
            player_Stats.Display_StaminaStats(sprint_Value);


            if (sprint_Value <= 0f) {
                sprint_Value = 0f;
                
                // reset the speed and sound
                player_Movement.speed = move_Speed;

                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;


            } // stamina runs out
        } else if (sprint_Value != 100f) {
            sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;
            player_Stats.Display_StaminaStats(sprint_Value);

            if (sprint_Value > 100f) {
                sprint_Value = 100f;
            }
        } 

    } // sprint

    void Crouch() {
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if (!is_Crouching) {
                look_Rotation.localPosition = new Vector3(0f, stand_Height, 0f);
                player_Movement.speed = move_Speed;

                is_Crouching = false;
            } else {
                look_Rotation.localPosition = new Vector3(0f, crouch_Height, 0f);
                player_Movement.speed = crouch_Speed;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;

                is_Crouching = true;
                
            }

        }
    } // crouch



}
