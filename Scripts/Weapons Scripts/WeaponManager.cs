using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    void Start() {

        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);

    } // start

    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            TurnOn_SelectedWeapon(0);}
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOn_SelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TurnOn_SelectedWeapon(2);      
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {   
            TurnOn_SelectedWeapon(3);       
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {      
            TurnOn_SelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            TurnOn_SelectedWeapon(5);
        }

    } // update

    void TurnOn_SelectedWeapon(int weaponIndex) {
        if (current_Weapon_Index == weaponIndex) 
            return;
        // turn off the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);
        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);
        // store the current selected weapon index
        current_Weapon_Index = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon() {
        return weapons[current_Weapon_Index];
    }


} // class
