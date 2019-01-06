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

	public int regenerationPoints = 0;

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

	public GameObject flower;

	/// abkürzungen etablieren
	private void Start()
	{
		gameData = new GameData(this);
		gameData.LoadGame();
		for (int i = 1; i < 21; i++) {
			GameObject go = (GameObject)Instantiate(flower, new Vector3(Random.Range(-50, 58), 0, Random.Range(403, 566)),
							   Quaternion.identity);
			go.name = "FlowerRed" + i;
			Debug.Log(go.name + " at " + go.transform.position.x + " " +
			go.transform.position.y + " " + go.transform.position.z);
		}

		rigid = GetComponent<Rigidbody>();
		m_CharacterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();

		AddQuest(
			new Quest("TheThirdKind", "You must search the wise green man"));
	}

	// Update is called once per frame
	private void Update()
	{
		// ChangeStamina (Time.deltaTime * 5);
		if (health < 50 && regenerationPoints > 0) {
			int p = Mathf.Min(regenerationPoints, 10);
			ChangeHealth(p);
			regenerationPoints -= p;
		}
		if (stamina < 50 & regenerationPoints > 0) {
			int p = Mathf.Min(regenerationPoints, 10);
			ChangeStamina(p);
			regenerationPoints -= p;
		}
		// überprüfe ob lebendig
		// x und z Koordinaten Bewegung
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;


		// Animationen
		anim.SetFloat("forward", z * 3);
		anim.SetBool("Walking", true);
		// raycast für "isgrounded"
		RaycastHit hit;
		// raycast für "Interact"
		RaycastHit interactHit;



		transform.Translate(x, 0, z);

		// Springen
		if (Input.GetAxis("Jump") > 0f) {
			// isgrounded: Vektor Richtung Boden mit Länge 1. 
			// Wenn er etwas trifft, ist isgrounded true (was bedeutet,
			// dass springen möglich ist). Wenn nicht, dann nicht. 

			if (Physics.Raycast(transform.position, Vector3.down, out hit, 1)) {
				Debug.DrawLine(transform.position, hit.point);
				print(hit.distance);
				Vector3 power = rigid.velocity;
				power.y = 5f;
				rigid.velocity = power;
			}
		}

		// Angriff
		if (Input.GetButtonDown("Fire1")) {
			// hole und vergleiche Waffenwerte
			GameObject weapon = GetComponent<WeaponManager>().GetActiveWeapon();
			float weaponstamina = weapon.GetComponent<Stats>().weaponStamina;
			string weaponname = weapon.GetComponent<Stats>().weaponName;
			if (stamina >= weaponstamina) {
				//Spiel Attackier-Animation
				Debug.Log("Player attacked");
				if (weaponname == "Katana" || weaponname == "Bo")
					anim.Play("Katana 0");
				else if (weaponname == "Hands")
					anim.Play("Katana 0");

				ChangeStamina(-weaponstamina);
			}
		}

		// Interagieren (Standard "E")
		if (Input.GetAxis("Interact") > 0f) {
			Vector3 forward = transform.TransformDirection(Vector3.forward);

			if (Physics.Raycast(transform.position, forward,
				out interactHit, 10)) {
				Debug.DrawLine(transform.position, interactHit.point);
				if (interactHit.collider.gameObject.tag == "Interactive") {
					// sende eine Nachricht
					interactHit.collider.gameObject.SendMessage("Interact",
						(Player)this);
					// Aufgabe aktualisieren - wenn sie existiert
					quests.Interacted(interactHit.collider.gameObject);
				}
			}

		}

		if (Input.GetAxis("Fire2") > 0f) {
			// spiel Attackier-Animation

			isBlocking = true;
			anim.SetBool("Blocking", true);
		}
		else {
			isBlocking = false;
			anim.SetBool("Blocking", false);
		}

		//	if (isBlocking == true) {
		//		Debug.Log ("Player Blocked");
		//		anim.Play ("Block");
		//	}
	}

	public void InitState()
	{
		transform.position = new Vector3(429, 0, 226);
		transform.rotation = Quaternion.identity;
		health = 100f;
		stamina = 100f;
		regenerationPoints = 0;
		gameObject.SetActive(true);
	}

	public void LoadState(GameData gameData)
	{
		gameData.LoadTransform("player", transform);
		health = gameData.LoadFloat("playerĥealth", 100f);
		stamina = gameData.LoadFloat("playerstamina", 100f);
		regenerationPoints = gameData.LoadInt("playerrp", 0);
	}

	public void SaveState(GameData gameData)
	{
		gameData.SaveTransform("player", transform);
		gameData.SaveFloat("playerĥealth", health);
		gameData.SaveFloat("playerstamina", stamina);
		gameData.SaveInt("playerrp", regenerationPoints);
	}

	public void ChangeHealth(float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) {
			Debug.Log("YOU ARE DEAD");
			gameData.InitNewGame();
		}
	}

	public void ChangeStamina(float change)
	{
		stamina += change;

		if (stamina > 100.0f)
			stamina = 100.0f;
		else if (stamina < 0.0f)
			stamina = 0.0f;
	}

	public void AddRegenerationPoints(int extra)
	{
		regenerationPoints += extra;
	}

	// Fügt der Liste eine neue Aufgabe hinzu
	public void AddQuest(Quest q)
	{
		quests.AddQuest(q);
	}

	// Sucht nach einer Aufgabe per Name
	public Quest GetQuest(string name)
	{
		return quests.GetQuest(name);
	}

	// Sucht nach einer aktiven Aufgabe per Name
	public Quest GetActiveQuest(string name)
	{
		return quests.GetActiveQuest(name);
	}

}