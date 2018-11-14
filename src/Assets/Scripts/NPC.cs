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
    
    //physikkomponente
    private Rigidbody rigid;
    //animationskomponente
    private Animator anim;
	private Collider fov;

	public Transform target;



    
    //find Player src https://www.quora.com/How-do-I-make-an-NPC-move-in-Unity#
    
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
		fov = FOV.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {    
		

		if (health < 0) 
		{
			Debug.Log ("NPC HEALTH: " + health);
			Destroy (AI);
		} 
        //TODO
        //animations
        //anim.SetFloat("forward", z);
        //Go towards Players'x
		if (hasseenplayer == true) 
		{
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
			transform.LookAt(target);//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
		} else { anim.SetBool ("iswalking", false);}


        
        
    }

	void OnTriggerEnter(Collider fov)
	{	
		if (fov.name == "Player") {
			Debug.Log ("has seen");
			hasseenplayer = true;
		}

	} 

	public void ChangeHealth(float change)
	{
		health += change;

		if (health > 100.0f)
			health = 100.0f;
		else if (health < 0.0f) 
		{
			Debug.Log("I'M DEAD");
			gameObject.SetActive (false);
		}
	}
}