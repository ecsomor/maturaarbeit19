using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour 
{
	//This script should be able to apply the damage taken from the ATCK value of the weapon and apply it to the Entity it collided with
	//The Entity that has the Stats


	//Placeholders for WeaponStats
	private float damage;
	private string name;
	private float defense;
	private float range;
	private float speed;
	private float stamina;




	public GameObject Self;
	
	//The Entity That is the Damaging area of the weapon (must have physics collider)

	void Start()
	{
		//Source https://answers.unity.com/questions/42843/referencing-non-static-variables-from-another-scri.html

		//pulling weapon Stats
		name = Self.GetComponent<Stats>().WeaponName;
		damage = Self.GetComponent<Stats>().WeaponDamage;//The Direct Damage applied
		defense = Self.GetComponent<Stats>().WeaponDefense;//The Damage Subtracted from Direct Damage while Blocking
		range = Self.GetComponent<Stats> ().WeaponRange;//TODO DELETE RANGE, GOING WITH COLLIDERS
		speed = Self.GetComponent<Stats> ().WeaponSpeed;
		stamina = Self.GetComponent<Stats> ().WeaponStamina;


	}

	void OnTriggerEnter(Collider col) 
		{
		Debug.Log ("Hit: " + col.name + " Weapon: " + name);
		if (col.name == "AI") {
			GameObject.Find (col.name).GetComponent<NPC> ().ChangeHealth( -damage );
			Debug.Log (damage + " Damage applied to " + col.name);
		} 
		else if (col.name == "Player") 
		{
			GameObject.Find (col.name).GetComponent<Player> ().ChangeHealth( -damage );
			Debug.Log (damage + " Damage applied to " + col.name);
		}



		//Debug.Log (secondstats.Weaponname);

		}
		
}
