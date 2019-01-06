using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{


	// Objekt bekommt einen Interaktionsaufruf
	void Interact(Player p)
	{
		p.AddRegenerationPoints(15);
		gameObject.SetActive(false);
	}
}
