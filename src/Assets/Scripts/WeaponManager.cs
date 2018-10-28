using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///This class Manages the availability and equipping of Weapons
public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponList;
    public List<GameObject> enabledWeaponList;

    /// Weapons
    public GameObject Bo;
    public GameObject Katana;
    public GameObject Hands;

    public string Equipped;


    public bool isplayer; //Bool to differ an NPC from the Player
    // Use this for initialization

    //inspired by https://forum.unity.com/threads/help-with-multiple-weapons-switching.465702/

    void Start()
    {
        weaponList = new List<GameObject>(); //list of all weapons	
        enabledWeaponList = new List<GameObject>(); //List of Weapons in the inventory


        weaponList.Add(Katana);
        weaponList.Add(Bo);
        weaponList.Add(Hands);


        enabledWeaponList.Add(Katana);
        enabledWeaponList.Add(Bo);
        enabledWeaponList.Add(Hands);

        setActiveWeapon(Katana); //equip weapon in hands
    }

    // Update is called once per frame
    void Update()
    {
        // Switching weapons
        if (isplayer == true)
        {
            if (Input.GetAxis("1") > 0f)
            {
                setActiveWeapon(Katana);
                Equipped = "Katana";
            }

            if (Input.GetAxis("2") > 0f)
            {
                setActiveWeapon(Bo);
                Equipped = "Bo";
            }

            if (Input.GetAxis("3") > 0f)
            {
                setActiveWeapon(Hands);
                Equipped = "Hands";
            }
        }
    }

//void to activate the activeWeapon and deactivate all the others, as else they would all be equipped at the same time
    private void setActiveWeapon(GameObject activeWeapon)
    {
        for (int j = 0; j < weaponList.Count; j++)
        {
            if (weaponList[j] == activeWeapon)
            {
                for (int i = 0; i < enabledWeaponList.Count; i++)
                {
                    if (enabledWeaponList[i] == activeWeapon)
                    {
                        weaponList[j].SetActive(true);
                    }
                }
            }
            else
            {
                weaponList[j].SetActive(false);
            }
        }
    }

//add a weapon to the available weapons list when walked over it
    public void pickedUpWeapon(GameObject weaponPickedUp)
    {
        enabledWeaponList.Add(weaponPickedUp);
    }




}