using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	public Player player;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Canvas> ().enabled = false;

	}

	/// <summary>
	/// Wahr, wenn die Taste bereits zuvor als gedrückt erkannt wurde
	/// Nötig, um Mehrfachauswertungen der Menütasten zu verhindern
	/// </summary>
	private bool keyWasPressed = false;
	
	// Update is called once per frame
	void Update ()
	{	
		
		if (Input.GetAxisRaw ("Menu") > 0f) {
			if (!keyWasPressed)
				GetComponent<Canvas> ().enabled = !GetComponent<Canvas> ().enabled;

			keyWasPressed = true;
		} else
			keyWasPressed = false;
	}
	// Beendetd das Spiel
	public void OnButtonEndPressed ()
	{
		Debug.Log ("Spiel beendet");
		Application.Quit ();
	}
	// Startet ein neues Spiel
	public void OnButtonNewPressed ()
	{
		
		player.gameData.InitNewGame ();
	}


	//Speicherfunktion
	public void OnButtonSavePressed ()
	{
		Debug.Log ("Speichern");
		player.gameData.SaveGame ();

//		GameData savegame = new GameData ();
//		savegame.SaveGame ();

	}
	
		
}
