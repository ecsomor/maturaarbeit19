using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {
	//This script should be able to apply the damage taken from the ATCK value of the weapon and apply it to the Entity it collided with
	//The Entity that has the Stats
	public GameObject Stats;
	
	//The Entity That is the Damaging area of the weapon (must have physics collider)
	public GameObject Collider;

}
