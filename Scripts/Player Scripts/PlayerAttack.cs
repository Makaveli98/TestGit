using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage;

    private Animator zoomCamera_Anim;
    private bool zoomed;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Spear_Startposition;

    private Camera mainCam;

    private GameObject crosshair;

    void Awake() {

        weapon_Manager = GetComponent<WeaponManager>();

        zoomCamera_Anim = GameObject.Find("FP Camera").GetComponent<Animator>();

        // zoomCamera_Anim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    }

    void Update() {

        WeaponShoot();
        Zoom_In_And_Out();
    }

    void WeaponShoot() {

        // if assault rifle
        if (weapon_Manager.GetCurrentSelectedWeapon().fire_Type == WeaponFireType.MULTIPLE) {
            
            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {
                
                nextTimeToFire = Time.time + 1f / fireRate; 

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();

            } // assault rifle

            // if we have a regular weapon that shoots once
        } else {

            if (Input.GetMouseButtonDown(0)) {
                
                // handle axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // handle shoot
                if (weapon_Manager.GetCurrentSelectedWeapon().bullet_Type == WeaponBulletType.BULLET) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();

                } else {

                    // we have an arrow or spear
                    if (is_Aiming) {
                        // if we are aiming play ShootAnimation
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                        
                        // also check what bullet_Type the weapon holds
                        // if arrow 
                        if (weapon_Manager.GetCurrentSelectedWeapon().bullet_Type == WeaponBulletType.ARROW) {
                            // throw arrow
                            Throw_Arrrow_Or_Spear(true);

                        } 
                        // if spear
                        else if (weapon_Manager.GetCurrentSelectedWeapon().bullet_Type == WeaponBulletType.SPEAR) {
                            // throw spear
                            Throw_Arrrow_Or_Spear(false);
                        }
                    }
                    
                } // else staement

                
            } // if input get mouse button 0
            
        } // else statement

    } // weapon shoot method

    void Zoom_In_And_Out() {

        // going to aim with our camera on the weapon (zoom in)
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            
            // if press and hold right mouse button
            if (Input.GetMouseButtonDown(1)) {
                zoomCamera_Anim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);
            }

            // when release the right mouse button click
            if (Input.GetMouseButtonUp(1)) {
                zoomCamera_Anim.Play(AnimationTags.ZOOM_OUT_ANIM);
                
                crosshair.SetActive(true);
            }

        } // if need to zoom the weapon

        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM) {

            if (Input.GetMouseButtonDown(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming = true;

            }

            if (Input.GetMouseButtonUp(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);

                is_Aiming = false;

            }

            
            
        } // weapon self aim


    } // zoom in and out

    

    void Throw_Arrrow_Or_Spear(bool throwArrow) {

        if (throwArrow) {
            
            GameObject arrow = Instantiate(arrow_Prefab, arrow_Spear_Startposition.transform.position, 
            arrow_Spear_Startposition.transform.rotation) as GameObject;

            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        } 
        else {

            GameObject spear = Instantiate(spear_Prefab, arrow_Spear_Startposition.transform.position, 
            arrow_Spear_Startposition.transform.rotation) as GameObject;

            spear.GetComponent<ArrowBowScript>().Launch(mainCam);

        }
    } // throw arrow or spear

    void BulletFired() {

        RaycastHit hit;

        // any data that 'out' gets will be put in the variable that is passed after out
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {
            print("We hitt: " + hit.transform.gameObject.name);

            if (hit.transform.gameObject.tag == Tags.ENEMY_TAG) {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }          
        }
    } // bullet fired



} // class
