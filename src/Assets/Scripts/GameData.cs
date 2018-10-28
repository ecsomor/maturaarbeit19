using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData 
{
	public GameData (Player p)
	{
		player = p;
	}

	private Player player;

	public void InitNewGame ()
	{
		player.InitState ();
	}

	public void LoadGame()
	{
		if (!PlayerPrefs.HasKey ("GameState"))
		{
			InitNewGame();
		}
		else
		{
			player.LoadState(this);
		}
	}

	public void SaveGame()
	{
		PlayerPrefs.SetInt ("GameState", 0);
		player.SaveState (this);
	}

	public void SaveTransform(string scope, Transform transform)
	{
		Vector3 position = transform.position;

		PlayerPrefs.SetFloat (scope+"X", position.x);
		PlayerPrefs.SetFloat (scope+"Y", position.y);
		PlayerPrefs.SetFloat (scope+"Z", position.z);
	}

	public void LoadTransform(string scope, Transform transform)
	{
		Vector3 position = new Vector3 (0, 0, 0);

		position.x = PlayerPrefs.GetFloat (scope+"X");
		position.y = PlayerPrefs.GetFloat (scope+"Y");
		position.z = PlayerPrefs.GetFloat (scope+"Z");
		transform.position = position;
	}
}
