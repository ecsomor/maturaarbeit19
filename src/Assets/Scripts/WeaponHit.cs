using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour 
{
	//This script should be able to apply the damage taken from the ATCK value of the weapon and apply it to the Entity it collided with
	//The Entity that has the Stats

	private float Damage;
	private string Name;
	public GameObject Self;
	
	//The Entity That is the Damaging area of the weapon (must have physics collider)

	void Start()
	{
		//Source https://answers.unity.com/questions/42843/referencing-non-static-variables-from-another-scri.html

		Name = Self.GetComponent<Stats>().WeaponName;
		Damage = Self.GetComponent<Stats> ().WeaponDamage;


	}

	void OnTriggerEnter(Collider col) 
		{
		Debug.Log ("Hit: " + col.name + " Weapon: " + Name);
		if (col.name == "AI") {
			GameObject.Find (col.name).GetComponent<NPC> ().NPCHealth -= Damage;
			Debug.Log (Damage + " Damage applied to " + col.name);
		} 
		else if (col.name == "Player") 
		{
			GameObject.Find (col.name).GetComponent<Player> ().Health -= Damage;
			Debug.Log (Damage + " Damage applied to " + col.name);
		}



		//Debug.Log (secondstats.Weaponname);

		}
		
}
