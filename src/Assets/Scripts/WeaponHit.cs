using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
	// Dieses Skript fügt Schaden in Höhe des WeaponDamage Werts 
	// einer Waffe einem Objekt zuzufügen

	// Platzhalter für Waffenwerte
	private float damage;
	private string weaponname;
	private float defense;
	private float range;
	private float speed;
	private float stamina;

	public GameObject Self;

	// Das Element, welches die Trefferzone darstellt, muss einen Physik 
	// Collider haben

	void Start()
	{
		// abfüllen der Stats
		weaponname = Self.GetComponent<Stats>().weaponName;
		damage = Self.GetComponent<Stats>().weaponDamage;
		defense = Self.GetComponent<Stats>().weaponDefense;
		range = Self.GetComponent<Stats>().weaponRange;
		speed = Self.GetComponent<Stats>().weaponSpeed;
		stamina = Self.GetComponent<Stats>().weaponStamina;
	}

	// Wenn der Weapon-Collider etwas trifft, überprüfe ob etwas mit dem Tag 
	// "Enemy" oder dem Namen "Player" getroffen wurde und füge Schaden zu
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("Hit: " + col.name + " Weapon: " + weaponname);
		if (col.tag == "Enemy") {
			col.gameObject.GetComponentInParent<NPC>().ChangeHealth(-damage);
			Debug.Log(damage + " Damage applied to " + col.name);
		}
		else if (col.name == "Player") {
			col.gameObject.GetComponentInParent<Player>().ChangeHealth(-damage);
			Debug.Log(damage + " Damage applied to " + col.name);
		}
	}

}
