using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Diese Klasse verwaltet die Verfügbarkeit und Ausstattung von Waffen
public class WeaponManager : MonoBehaviour
{
	public List<GameObject> weaponList;
	public List<GameObject> enabledWeaponList;

	/// Waffen
	public GameObject Bo;
	public GameObject Katana;
	public GameObject Hands;

	public string equipped;

	private GameObject weaponInHand;

	// Bool zur Unterscheidung von NPC und Player 
	public bool isplayer;

	// inspiriert von 
	// https://forum.unity.com/threads/help-with-multiple-weapons-switching.465702/

	void Start ()
	{
		weaponList = new List<GameObject> (); // alle Waffen	
		enabledWeaponList = new List<GameObject> (); // alle verfügbaren Waffen


		weaponList.Add (Katana);
		weaponList.Add (Bo);
		weaponList.Add (Hands);


		enabledWeaponList.Add (Katana);
		enabledWeaponList.Add (Bo);
		enabledWeaponList.Add (Hands);

		// mit Waffe ausrüsten
		setActiveWeapon (Katana);  
	}

	void Update ()
	{
		// Wechseln der Waffe
		if (isplayer == true) {
			if (Input.GetAxis ("1") > 0f) {
				setActiveWeapon (Katana);
				equipped = "Katana";
			}

			if (Input.GetAxis ("2") > 0f) {
				setActiveWeapon (Bo);
				equipped = "Bo";
			}

			if (Input.GetAxis ("3") > 0f) {
				setActiveWeapon (Hands);
				equipped = "Hands";
			}
		}
	}

	// die activeWeapon aktivieren und anderen zu deaktivieren, sonst 
	// erscheinen sie gleichzeitig
	private void setActiveWeapon (GameObject activeWeapon)
	{
		for (int j = 0; j < weaponList.Count; j++) {
			if (weaponList [j] == activeWeapon) {
				for (int i = 0; i < enabledWeaponList.Count; i++) {
					if (enabledWeaponList [i] == activeWeapon) {
						weaponList [j].SetActive (true);
						weaponInHand = activeWeapon;
					}
				}
			} else {
				weaponList [j].SetActive (false);
			}
		}
	}

	// füge eine Waffe zu der Liste der verfügbaren Waffen, 
	// wenn man darüber läuft
	public void pickedUpWeapon (GameObject weaponPickedUp)
	{
		enabledWeaponList.Add (weaponPickedUp);
	}

	public GameObject getActiveWeapon ()
	{
		return weaponInHand;
	}
}