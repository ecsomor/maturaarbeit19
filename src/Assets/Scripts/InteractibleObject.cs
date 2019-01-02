using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{


	// Objekt bekommt einen Interaktionsaufruf
	void Interact (Player p)
	{
		gameObject.SetActive (false);
	}
}
