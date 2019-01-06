using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
	private GameObject[] enemies;
	private TheThirdKind theThirdKind;

	public GameData(Player p)
	{
		player = p;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] interactives = GameObject.FindGameObjectsWithTag("Interactive");
		foreach( GameObject go in interactives ){
			if( go.name == "TheThirdKind" ) {
				theThirdKind = go.GetComponent<TheThirdKind>();
				break;
			}
		}
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
		theThirdKind.InitState(player);
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
			theThirdKind.LoadState(this,player);
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
		theThirdKind.SaveState(this,player);

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

	public void SaveBool(string scope, bool b)
	{
		PlayerPrefs.SetInt(scope, b ? 1 : 0);
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

	public bool LoadBool(string scope, bool def)
	{
		if (PlayerPrefs.HasKey(scope))
			return PlayerPrefs.GetInt(scope) != 0;
		else
			return def;
	}

}
