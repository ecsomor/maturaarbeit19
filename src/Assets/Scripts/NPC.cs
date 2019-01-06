using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class NPC : MonoBehaviour
{
	// Körperteile
	public GameObject AI;
	public GameObject AITorso;
	public GameObject AILeftFoot;
	public GameObject AIRightFoot;
	public GameObject AILeftHand;
	public GameObject AIRightHand;

	//Spieler Objelt
	public GameObject player;

	// Field-of-View (kann sehen)
	public GameObject FOV;

	// Initialisiere NPC Values
	public float health = 100f;
	public float stamina = 100f;
	public float speed = 2.0f;
	public bool hasseenplayer = false;
	public bool fighting = false;

	// Physikkomponente
	private Rigidbody rigid;

	// Animationskomponente
	private Animator anim;
	private Collider fov;

	public Transform target;
	public RaycastHit hit;

	private Transform originalTransform;

	// find Player src https://www.quora.com/How-do-I-make-an-NPC-move-in-Unity# 
	private void Start()
	{
		// Verbinde Komponenten
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		fov = FOV.GetComponent<Collider>();
		player = GameObject.FindGameObjectWithTag("Player");
		FreedomQuest.npcsCreated += 1;
		name = "A1_" + FreedomQuest.npcsAlive;
		originalTransform = gameObject.transform;
	}

	private void Update()
	{
		// wenn Gesundheit unter 0, zerstöre Objekt
		if (health < 0) {
			Debug.Log("NPC HEALTH: " + health);
			Destroy(AI);
		}

		if (hasseenplayer == true) {
			// wenn Spieler in Reichweite, angreifen (fighting = true )
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position, forward, out hit, 10)) {

				Debug.DrawLine(transform.position, hit.point);
				if (hit.collider.gameObject.tag == "Player") {
					fighting = true;
					Debug.Log("NPC is attacking");
				}
				else {
					fighting = false;
					anim.ResetTrigger("Attack");
				}

				if (fighting == true) {
					// Animation Angriff starten
					anim.SetTrigger("Attack");
				}
				else {
					// Animation Laufen starten
					anim.SetBool("iswalking", true);

					if (player.transform.position.x >
						(transform.position.x - 1)) {
						// nach rechts
						transform.position +=
							new Vector3(speed * Time.deltaTime, 0, 0);

					}
					else {
						// nach links
						transform.position -=
							new Vector3(speed * Time.deltaTime, 0, 0);
					}

					// in Richtung Spieler
					if (player.transform.position.z > transform.position.z) {
						// nach oben
						transform.position +=
							new Vector3(0, 0, speed * Time.deltaTime);

					}
					else {
						// nach unten
						transform.position -=
							new Vector3(0, 0, speed * Time.deltaTime);
					}
					// https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
					transform.LookAt(target);
				}
			}
			else {
				anim.SetBool("iswalking", false);
			}




		}
	}

	// Entdecken des Spielers
	void OnTriggerEnter(Collider fov)
	{
		if (fov.name == "Player") {
			Debug.Log("has seen");
			hasseenplayer = true;
		}

	}

	public void ChangeHealth(float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) {
			Debug.Log(gameObject.name + " I'M DEAD");
			gameObject.SetActive(false);
			FreedomQuest.npcsAlive -= 1;

			Player p = player.GetComponent<Player>();
			// Dem Player extra Punkte geben
			p.AddRegenerationPoints(10);
		}
	}

	public void InitState()
	{
		health = 100f;
		stamina = 100f;
		hasseenplayer = false;
		fighting = false;
		gameObject.SetActive(true);
		transform.SetPositionAndRotation(originalTransform.position,
			originalTransform.rotation);
	}

	public void LoadState(GameData gameData)
	{
		gameData.LoadTransform(name, transform);
		health = gameData.LoadFloat(name + "ĥealth", 100f);
		stamina = gameData.LoadFloat(name + "stamina", 100f);
		gameObject.SetActive(gameData.LoadBool(name + "active", true));
		hasseenplayer = gameData.LoadBool(name + "hasseenplayer", false);
		fighting = gameData.LoadBool(name + "fighting", false);
	}

	public void SaveState(GameData gameData)
	{
		gameData.SaveTransform(name, transform);
		gameData.SaveFloat(name + "ĥealth", health);
		gameData.SaveFloat(name + "stamina", stamina);
		gameData.SaveBool(name + "active", gameObject.activeSelf);
		gameData.SaveBool(name + "hasseenplayer", hasseenplayer);
		gameData.SaveBool(name + "fighting", fighting);
	}

}