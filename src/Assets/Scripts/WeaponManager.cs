using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public List<GameObject> weaponList;
	public List<GameObject> enabledWeaponList;

	/// <summary>
	/// Weapons
	/// </summary>
	public GameObject Bo;
	public GameObject Katana;
	public GameObject Hands;



	public bool isplayer;
	// Use this for initialization
	void Start () {
		weaponList = new List<GameObject> ();
		enabledWeaponList = new List<GameObject> ();

		weaponList.Add (Katana);
		weaponList.Add (Bo);
		weaponList.Add (Hands);


		enabledWeaponList.Add(Katana);
		enabledWeaponList.Add(Bo);
		enabledWeaponList.Add (Hands);

		setActiveWeapon (Katana);
	}
	
	// Update is called once per frame
	void Update () {

		if (isplayer == true) {
			if (Input.GetAxis ("1") > 0f) {
				setActiveWeapon (Katana);
			}
			if (Input.GetAxis ("2") > 0f) {
				setActiveWeapon (Bo);
			}
			if (Input.GetAxis ("3") > 0f) {
				setActiveWeapon (Hands);
			}
		}
	}


private void setActiveWeapon(GameObject activeWeapon)
{
	for (int j = 0; j < weaponList.Count; j++)
	{
		if(weaponList[j] == activeWeapon)
		{
			for (int i = 0; i < enabledWeaponList.Count; i++)
			{
				if(enabledWeaponList[i] == activeWeapon)
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


public void pickedUpWeapon(GameObject weaponPickedUp)
{
	enabledWeaponList.Add(weaponPickedUp);
}
}