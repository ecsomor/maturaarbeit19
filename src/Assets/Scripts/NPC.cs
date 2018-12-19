using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class NPC : MonoBehaviour
{
	//Bodyparts
	public GameObject AI;
	public GameObject AITorso;
	public GameObject AILeftFoot;
	public GameObject AIRightFoot;
	public GameObject AILeftHand;
	public GameObject AIRightHand;
    
	//Player Object
	public GameObject player;

	//FOV
	public GameObject FOV;
    
	//Initialize NPC Values
	public float health = 100f;
	public float stamina = 100f;
	public float Speed = 2.0f;
	public bool hasseenplayer = false;
	public bool fighting = false;
    
	//physikkomponente
	private Rigidbody rigid;
	//animationskomponente
	private Animator anim;
	private Collider fov;

	public Transform target;
	public RaycastHit hit;



    
	//find Player src https://www.quora.com/How-do-I-make-an-NPC-move-in-Unity#
    
    
	private void Start ()
	{	
		//connect components
		rigid = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		fov = FOV.GetComponent<Collider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	private void Update ()
	{    
		
		//if health is under 0, delete Gameobject
		if (health < 0) {
			Debug.Log ("NPC HEALTH: " + health);
			Destroy (AI);
		} 
		//TODO
		//animations
		//anim.SetFloat("forward", z);
		//Go towards Players'x
		if (hasseenplayer == true) {	
			//if player is in range, attack (set fighting true)
			Vector3 forward = transform.TransformDirection (Vector3.forward);

			//check if "player" is in Range to attack, if so do so
			if (Physics.Raycast (transform.position, forward, out hit, 10)) {	

				Debug.DrawLine (transform.position, hit.point);
				if (hit.collider.gameObject.tag == "Player") {
					fighting = true;
					Debug.Log ("NPC is attacking");
				} else {
					fighting = false;
					anim.ResetTrigger ("Attack");
				}

				//if fight is true fight,
				if (fighting == true) {
					anim.SetTrigger ("Attack");
					//fight

				} else {
				
					anim.SetBool ("iswalking", true);
					
					if (player.transform.position.x > (transform.position.x - 1)) {
						//go right
						transform.position += new Vector3 (Speed * Time.deltaTime, 0, 0);
            		
					} else {
						//Go left
						transform.position -= new Vector3 (Speed * Time.deltaTime, 0, 0);
					}
					//go towards Players'z
					if (player.transform.position.z > transform.position.z) {
						//Go up
						transform.position += new Vector3 (0, 0, Speed * Time.deltaTime);
            		
					} else {
						//go down
						transform.position -= new Vector3 (0, 0, Speed * Time.deltaTime);
					}
					transform.LookAt (target);//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
				}
			} else {
				anim.SetBool ("iswalking", false);
			}


        
        
		}
	}
	//Spotting player
	void OnTriggerEnter (Collider fov)
	{	
		if (fov.name == "Player") {
			Debug.Log ("has seen");
			hasseenplayer = true;
		}

	}

	public void ChangeHealth (float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) {
			Debug.Log ("I'M DEAD");
			gameObject.SetActive (false);

			Player p = player.GetComponent<Player> ();
			// is killing this NPC was a quest, update it
			Quest q = p.GetActiveQuest(gameObject.name);
			if ( q != null )
				q.Done();

			//add money to the players balance
			p.money += 10;
		}
	}
}