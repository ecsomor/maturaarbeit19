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
    
    //Initialize NPC Values
    public float NPCHealth = 100f;
    public float NPCStamina = 100f;
    public float Speed = 2.0f;
    
    //physikkomponente
    private Rigidbody rigid;
    //animationskomponente
    private Animator anim;
    
    //find Player src https://www.quora.com/How-do-I-make-an-NPC-move-in-Unity#
    
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {    

		if (NPCHealth < 0) 
		{
			Debug.Log ("NPC HEALTH: " + NPCHealth);
			Destroy (AI);
		} 
        //TODO
        //animations
        //anim.SetFloat("forward", z);
        //Go towards Players'x
		if (player.transform.position.x > (transform.position.x-1))
        {
            //go right
            transform.position += new Vector3(Speed*Time.deltaTime,0,0);
            
        }
        else
        {
            //Go left
            transform.position -= new Vector3(Speed*Time.deltaTime,0,0);
        }
        //go towards Players'z
        if (player.transform.position.z > transform.position.z)
        {
            //Go up
            transform.position += new Vector3(0, 0, Speed*Time.deltaTime);
            
        }
        else
        {
            //go down
            transform.position -= new Vector3(0, 0, Speed*Time.deltaTime);
        }
        
        
    }
}