using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		GetComponent<Canvas>().enabled = false;

	}
	/// <summary>
	/// Wahr, wenn die Taste bereits zuvor als gedrückt erkannt wurde
	/// Nötig, um mehrfachauswertungen der Menütasten zu verhindern
	/// </summary>
	private bool keyWasPressed = false;
	
	// Update is called once per frame
	void Update ()
	{	
		
		if (Input.GetAxisRaw("Menu") > 0f)
		{
			if (!keyWasPressed)
				GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;

			keyWasPressed = true;
		}
		else
			keyWasPressed = false;
	}
	//Ends game
	public void OnButtonEndPressed ()
	{
		Debug.Log("Spiel Beendet");
		Application.Quit();
	}
	//Starts a new game
	public void OnButtonNewPressed()
	{
		//TODO
		print("Write some code here");
	}
	
		
}
