using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
} // outside class

public class EnemyController : MonoBehaviour {

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_Adudio;

    void Awake() {

        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        enemy_Adudio = GetComponentInChildren<EnemyAudio>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start() {

        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        // when the enemy first get to the player
        // attack right away
        attack_Timer = wait_Before_Attack;

        // memorize the value of chase distance
        // so that we can edit it
        current_Chase_Distance = chase_Distance;
    }

    void Update() {

        if (enemy_State == EnemyState.PATROL) {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE) {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }

    } // update

    void Patrol() {

        // tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        // add time to the patrol timer
        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time) { //which it always is 
            SetNewRandomDestination();
            patrol_Timer = 0f;
            
        }

        // sqrMagnitude = returns the square length of this vector(in this case NavMeshAgent)
        // if any axis has a value then the sqrMagnitude will have a value
        // that is > than 0
        // there will be a value in the sqrMagnitude that'll greater than 0
        if (navAgent.velocity.sqrMagnitude > 0) {
            enemy_Anim.Walk(true);
        
        } 
        else {
            enemy_Anim.Walk(false);
        }

        // test the distance between the player and the enemy
        // if the distance between and the player is less than chase distance
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance) {

            enemy_Anim.Walk(false);
            enemy_State = EnemyState.CHASE;

            //play spotted audio
            enemy_Adudio.Play_ScreamSound();
        }
        
    } // patrol

    void Chase() {

        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // set the player's(target) position as the destination
        // because we are chasing(running towards) the player
        navAgent.SetDestination(target.position);

        // if navAgent moving then animate the enemy
        if (navAgent.velocity.sqrMagnitude > 0) {
            enemy_Anim.Run(true);
        
        } else {
            enemy_Anim.Run(false);

        } // else statement

        // test the distance between the player and the enemy
        // if the distance between enemy and the player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance) {

            //  then stop animations run and walk
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance back to what it was
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
                
            }

        } else if (Vector3.Distance(transform.position, target.position) > chase_Distance) {
            // player run away from enemy

            // stop running
            enemy_Anim.Run(false);
            enemy_State = EnemyState.PATROL;

            // if enemy stops running 
            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;

            } // reset the chase distance again

        } // else statement

    } // chase

    void Attack() {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack) {

            enemy_Anim.Attack();

            attack_Timer = 0;

            //play attack sound
            enemy_Adudio.Play_AttackSound();
    
        }

        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance) {
            
            enemy_State = EnemyState.CHASE;

        }

    } // attack

void SetNewRandomDestination() {

    float random_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
    Vector3 randomDir = Random.insideUnitSphere * random_Radius;

    // use random direction which is generated in the code up
    // AND add the current position to it 
    randomDir += transform.position;

    NavMeshHit navHit;

    // will check the position in every layer THEN
    // it will take the randomDir position AND
    // check or return a SamplePosition within the range
    // of random_Radius inside the position of randomDir AND
    // make sure it's inside the NasvMeshAgent area;

    NavMesh.SamplePosition(randomDir, out navHit, random_Radius, -1);

    // navHitt wil take the data for a new position within randomDir
    navAgent.SetDestination(navHit.position);

}

void Turn_On_AttackPoint() {

    attack_Point.SetActive(true);
}

void Turn_Off_AttackPoint() {
    
    if (attack_Point.activeInHierarchy) {
        
        attack_Point.SetActive(false);
        
    }
}

public EnemyState Enemy_State {
    get {
        return enemy_State;
    }
    set {
        enemy_State = value;
    }
}

} // class
