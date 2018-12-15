using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// object is in interaction with player
	void Interact (Player p)
	{
		gameObject.SetActive (false);
	}
}
