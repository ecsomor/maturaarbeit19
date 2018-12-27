using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

// Steuerung der Spielfigur
public class Player : MonoBehaviour
{
	private CharacterController m_CharacterController;

	// Auf dem boden?
	private bool isGrounded;

	// blockt der Spieler?
	public bool isBlocking;

	// Laufgeschwindigkeit
	public float speed = 3f;
	//private float towardsY = 0f;

	// Sprungkraft
	public float sprungkraft = 5f;

	// Gesundheit
	public float health = 100f;

	// Ausdauer
	public float stamina = 100f;

	public int money = 0;

	// Das Graphische Modell, ua für drehung in Laufrichtung
	public GameObject model;

	// Zeiger auf die animations komponente der spielfigur
	private Animator anim;

	// Physikkomponente
	private Rigidbody rigid;

	// Der Winkel zu dem sich die Figur um die eigene Achse (=Y) drehen soll
	public float towardsY = 90f;

	public string lastTalk;


	public GameData gameData;

	public Quests quests;

	/// abkürzungen etablieren
	private void Start ()
	{
		gameData = new GameData (this);
		gameData.LoadGame ();

		rigid = GetComponent<Rigidbody> ();
		m_CharacterController = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();

		AddQuest (
			new Quest ("TheThirdKind", "You must search the wise green man"));
	}

	// Update is called once per frame
	private void Update ()
	{
		ChangeStamina (Time.deltaTime * 5);
		// überprüfe ob lebendig
		// x und z Koordinaten Bewegung
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * speed;
        

		// Animationen
		anim.SetFloat ("forward", z * 3);
		anim.SetBool ("Walking", true);
		// raycast für "isgrounded"
		RaycastHit hit;
		// raycast für "Interact"
		RaycastHit interactHit;



		transform.Translate (x, 0, z);

		// Springen
		if (Input.GetAxis ("Jump") > 0f) {
			// isgrounded: Vektor Richtung Boden mit Länge 1. 
			// Wenn er etwas trifft, ist isgrounded true (was bedeutet,
			// dass springen möglich ist). Wenn nicht, dann nicht. 

			if (Physics.Raycast (transform.position, Vector3.down, out hit, 1)) {
				Debug.DrawLine (transform.position, hit.point);
				print (hit.distance);
				Vector3 power = rigid.velocity;
				power.y = 5f;
				rigid.velocity = power;
			}
		}
        
		// Angriff
		if (Input.GetButtonDown ("Fire1")) {	
			// hole und vergleiche Waffenwerte
			GameObject weapon = GetComponent<WeaponManager> ().getActiveWeapon();
			float weaponstamina = weapon.GetComponent<Stats> ().WeaponStamina;
			string weaponname = weapon.GetComponent<Stats> ().WeaponName;
			if (stamina >= weaponstamina) {
				//Spiel Attackier-Animation
				Debug.Log ("Player attacked");
				if (weaponname == "Katana" || weaponname == "Bo")
					anim.Play ("Katana 0");
				else if (weaponname == "Shuriken")
					anim.Play ("Katana 0"); 

				ChangeStamina (-weaponstamina);
			}
		}

		// Interagieren (Standard "E")
		if (Input.GetAxis ("Interact") > 0f) {
			Vector3 forward = transform.TransformDirection (Vector3.forward);

			if (Physics.Raycast (transform.position, forward, 
				out interactHit, 10)) {	
				Debug.DrawLine (transform.position, interactHit.point);
				if (interactHit.collider.gameObject.tag == "Interactive") {
					// sende eine Nachricht
					interactHit.collider.gameObject.SendMessage ("Interact", 
						(Player)this);
					// Aufgabe aktualisieren - wenn sie existiert
					quests.Interacted (interactHit.collider.gameObject);
				}
			}
				
		}

		if (Input.GetAxis ("Fire2") > 0f) {
			// spiel Attackier-Animation

			isBlocking = true;
			anim.SetBool ("Blocking", true);
		} else {
			isBlocking = false;
			anim.SetBool ("Blocking", false);
		}

		//	if (isBlocking == true) {
		//		Debug.Log ("Player Blocked");
		//		anim.Play ("Block");
		//	}
	}

	public void InitState ()
	{
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	public void LoadState (GameData gameData)
	{
		gameData.LoadTransform ("player", transform);
	}

	public void SaveState (GameData gameData)
	{
		gameData.SaveTransform ("player", transform);
	}

	public void ChangeHealth (float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) {
			Debug.Log ("YOU ARE DEAD");
			model.SetActive (false);
		}
	}

	public void ChangeStamina (float change)
	{
		stamina += change;

		if (stamina > 100.0f)
			stamina = 100.0f;
		else if (stamina < 0.0f)
			stamina = 0.0f;
	}

	// Fügt der Liste eine neue Aufgabe hinzu
	public void AddQuest (Quest q)
	{
		quests.AddQuest (q);
	}

	// Sucht nach einer Aufgabe per Name
	public Quest GetQuest (string name)
	{
		return quests.GetQuest (name);
	}

	// Sucht nach einer aktiven Aufgabe per Name
	public Quest GetActiveQuest (string name)
	{
		return quests.GetActiveQuest (name);
	}

}