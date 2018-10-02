using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


// steuerung der Spielfigur
public class Player : MonoBehaviour
{
    private CharacterController m_CharacterController;


    //Auf dem boden?
    private bool isGrounded;

    //Laufgeschwindigkeit
    public float speed = 3f;
    //private float towardsY = 0f;

    //sprungkraft
    public float sprungkraft = 5f;

    //Health
    public float Health = 100f;

    //Stamina
    public float Stamina = 100f;

    //Das Graphische Modell, ua für drehung in Laufrichtung
    public GameObject model;

    //zeiger auf die animations komponente der spielfigur
    private Animator anim;

    //Physikkomponente
    private Rigidbody rigid;

    //Der winkel zu dem sich die figur um die eigene achse (=Y) drehen soll
    public float towardsY = 90f;

    /// weaponsystem initiation
    List<string> GotWeapons = new List<string>();

    public GameObject BoInHand;
    public GameObject KatanaInHand;
    public int EquippedWeapon = 0;

    public int WeaponSwitchCoolDown = 3;


    ///mostly establishing shortcuts
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        m_CharacterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //x and z coordinates movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //animations
        anim.SetFloat("forward", z);

        //raycast for "isgrounded"
        RaycastHit hit;


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