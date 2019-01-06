using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
	private GameObject[] enemies;

	public GameData(Player p)
	{
		player = p;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

	private Player player;
	// Neues Speicherfile
	public void InitNewGame()
	{
		player.InitState();
		foreach (GameObject go in enemies) {
			NPC npc = go.GetComponent<NPC>();
			npc.InitState();
		}
	}
	// Laden des Speicherfiles
	public void LoadGame()
	{
		if (!PlayerPrefs.HasKey("GameState")) {
			InitNewGame();
		}
		else {
			player.LoadState(this);
			foreach (GameObject go in enemies) {
				NPC npc = go.GetComponent<NPC>();
				npc.LoadState(this);
			}
		}
	}
	// Schreiben des Speicherfiles
	public void SaveGame()
	{
		PlayerPrefs.SetInt("GameState", 0);
		player.SaveState(this);
		foreach (GameObject go in enemies) {
			NPC npc = go.GetComponent<NPC>();
			npc.SaveState(this);
		}
	}
	// Speichern der Koordinaten
	public void SaveTransform(string scope, Transform transform)
	{
		Vector3 position = transform.position;

		PlayerPrefs.SetFloat(scope + "X", position.x);
		PlayerPrefs.SetFloat(scope + "Y", position.y);
		PlayerPrefs.SetFloat(scope + "Z", position.z);
	}

	public void SaveFloat(string scope, float f)
	{
		PlayerPrefs.SetFloat(scope, f);
	}

	public void SaveInt(string scope, int i)
	{
		PlayerPrefs.SetInt(scope, i);
	}

	// Laden der Position
	public void LoadTransform(string scope, Transform transform)
	{
		Vector3 position = new Vector3(0, 0, 0);
		if (PlayerPrefs.HasKey(scope + "X")) {
			position.x = PlayerPrefs.GetFloat(scope + "X");
			position.y = PlayerPrefs.GetFloat(scope + "Y");
			position.z = PlayerPrefs.GetFloat(scope + "Z");
			transform.position = position;
		}
	}

	public float LoadFloat(string scope, float def)
	{
		if (PlayerPrefs.HasKey(scope))
			return PlayerPrefs.GetFloat(scope);
		else
			return def;
	}

	public int LoadInt(string scope, int def)
	{
		if (PlayerPrefs.HasKey(scope))
			return PlayerPrefs.GetInt(scope);
		else
			return def;
	}


}
