using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


// steuerung der Spielfigur
public class Player : MonoBehaviour
{
    private CharacterController m_CharacterController;

    //Auf dem boden?
    private bool isGrounded;

	//blockt der Spieler?
	public bool isBlocking;

    //Laufgeschwindigkeit
    public float speed = 3f;
    //private float towardsY = 0f;

    //sprungkraft
    public float sprungkraft = 5f;

    //Health
    public float health = 100f;

    //Stamina
    public float stamina = 100f;

    //Das Graphische Modell, ua für drehung in Laufrichtung
    public GameObject model;

    //zeiger auf die animations komponente der spielfigur
    private Animator anim;

    //Physikkomponente
    private Rigidbody rigid;

    //Der winkel zu dem sich die figur um die eigene achse (=Y) drehen soll
    public float towardsY = 90f;

    // weaponsystem initiation
   // List<string> GotWeapons = new List<string>();

    //public GameObject BoInHand;
    //public GameObject KatanaInHand;
   // public int EquippedWeapon = 0;

   /// public int WeaponSwitchCoolDown = 3;

	public GameData gameData;

	public Quests quests;

    ///mostly establishing shortcuts
    private void Start()
    {
		gameData = new GameData (this);
		gameData.LoadGame ();

        rigid = GetComponent<Rigidbody>();
        m_CharacterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
		ChangeStamina (Time.deltaTime * 5 );
		//Check if alive
        //x and z coordinates movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        

        //animations
		anim.SetFloat("forward", z*3);
        anim.SetBool("Walking", true);
        //raycast for "isgrounded"
        RaycastHit hit;
		//Raycast for "Interact"
		RaycastHit interactHit;


        //transform.Rotate(0,x,0);
        transform.Translate(x, 0, z);

        //Springen
        if (Input.GetAxis("Jump") > 0f)
        {
            //isgrounded: Vector to the ground with length 1. If it hits something, isgrounded will be true (which means jumping is possible), if not then not.
            Vector3 fwd = transform.TransformDirection(Vector3.down);


            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
            {
                Debug.DrawLine(transform.position, hit.point);
                print(hit.distance);
                Vector3 power = rigid.velocity;
                power.y = 5f;
                rigid.velocity = power;
            }
        }
        
        //attack
		// TODO: either block entering second attack or do not substract full stamina
		if (Input.GetButtonDown("Fire1") )
        {
			GameObject weapon = GetComponent<WeaponManager>().getActiveWeapon ();
			float weaponstamina = weapon.GetComponent<Stats> ().WeaponStamina;
			string weaponname = weapon.GetComponent<Stats> ().WeaponName;
			if (stamina >= weaponstamina) 
			{
				//Play attack animation
				Debug.Log ("Player attacked");
				//anim.SetInteger ("Attack", 1);
				if ( weaponname == "Katana" || weaponname == "Bo" )
					anim.Play ("Katana 0");
				else if ( weaponname == "Shuriken" )
					anim.Play ("Katana 0"); // TODO: shuriken animation

				ChangeStamina (-weaponstamina);


			}
        }

		//interact
		if (Input.GetAxis ("Interact") > 0f) {
			Vector3 forward = transform.TransformDirection(Vector3.forward);


			if (Physics.Raycast(transform.position, forward, out interactHit, 10))
			{	
				
				Debug.DrawLine(transform.position, interactHit.point);
				if (interactHit.collider.gameObject.tag == "Interactive") {
					interactHit.collider.gameObject.SendMessage ("Interacted");
					quests.Interacted (interactHit.collider.gameObject);
					Debug.Log ("Player used 'Interact'");//TODO

				}

			}





		}


		if (Input.GetAxis ("Fire2") > 0f) {
			//Play attack animation

			isBlocking = true;
			anim.SetBool("Blocking", true);
		} else {
			isBlocking = false;
			anim.SetBool ("Blocking", false);
		}

	//	if (isBlocking == true) {
	//		Debug.Log ("Player Blocked");
	//		anim.Play ("Block");
	//	}
        
        
    }

	public void InitState()
	{
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	public void LoadState(GameData gameData)
	{
		gameData.LoadTransform ("player", transform);
	}

	public void SaveState(GameData gameData)
	{
		gameData.SaveTransform ("player", transform);
	}

	public void ChangeHealth(float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) 
		{
			Debug.Log("YOU ARE DEAD");
			model.SetActive (false);
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

    //transform.position += h * speed * transform.forward;

    //if (h > 0f) // nach rechts gehen 
    //	towardsY = 0f;
    //else if (h < 0f) //nach links gehen
    //	towardsY = 180f;

    //model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime);


//	void WeaponSwitch(){
//
//
//		///Managing equip
//		if (EquippedWeapon == 0) {
//
//			BoInHand.SetActive (false);
//			KatanaInHand.SetActive (false);
//
//		}
//		else if (EquippedWeapon == 1) {
//			if (GotWeapons.Contains ("Katana")) {
//				BoInHand.SetActive (false);
//				KatanaInHand.SetActive (true);
//
//			} 
//		}
//		else if (EquippedWeapon == 2) {
//			if (GotWeapons.Contains ("Bo")) 
//			{
//				BoInHand.SetActive (true);
//				KatanaInHand.SetActive (false);
//
//			}
//
//
//		}
//		else if (EquippedWeapon > 2) {
//			EquippedWeapon = 0;
//
//		}
//		
//	}
//
//	void OnTriggerEnter(Collider other) 
//	{
//		if (other.gameObject.CompareTag("Pickup")) 
//		{
//			
//			other.gameObject.SetActive(false);
//			if (other.gameObject.name == "Bo-Pickup") 
//			{
//				
//
//			}
//			else if (other.gameObject.name == "Katana-pickup")
//			{
//				GotWeapons.Add ("Katana");
//				EquippedWeapon = 1;
//			}
//				
//		
//		}
//	}
}

//Destroy(other.gameObject);
//