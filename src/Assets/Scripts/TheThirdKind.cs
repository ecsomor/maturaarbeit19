using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdKind : InteractibleObject
{
	private GameObject flowerRed;
	private GameObject flowerBlue;

	void Start()
	{
		GameObject[] interactives = GameObject.FindGameObjectsWithTag("Interactive");
		foreach( GameObject go in interactives ){
			if ( go.name == "FlowerRed" ) {
				flowerRed = go;
			}
			else if ( go.name == "FlowerBlue" ) {
				flowerBlue = go;
			}
		}
	}

	void Update()
	{

	}

	private void CreateDummyQuest(Player p)
	{
		p.AddQuest(
			new Quest("TheThirdKind", "You must search the wise green man"));
		p.lastTalk = "You must search the wise green man";
	}

	private void CreateFirstQuest(Player p)
	{
		p.AddQuest(new Quest("FlowerRed", "Go and pick the flower behind the house"));
		p.AddQuest(new Quest("FlowerBlue", "Go and pick the flower at the corner"));

		p.AddQuest(new Quest("TheThirdKind", "You must come back to the wiser green man"));
		p.lastTalk = "You must pick the two flowers and come back to me. The picked flowers give you regeneration points";
	}

	private void CreateSecondQuest(Player p)
	{
		p.AddQuest(new FreedomQuest("You must fight all red figures"));
		p.lastTalk = "Great, now you must fight the red figures. Pick the flowers you find to get regeneration points";
	}

	void Interact(Player p)
	{
		Quest qFlowerRed = p.GetQuest("FlowerRed");
		Quest qFlowerBlue = p.GetQuest("FlowerBlue");

		if (qFlowerRed == null) {
			CreateFirstQuest(p);
		}
		else if (qFlowerRed.IsDone() && qFlowerBlue.IsDone()) {
			Quest enemies = p.GetQuest("A1");
			// Beim ersten mal zurückkommen wenn die Aufgabe noch nicht erledigt ist 
			// wird einmal durchlaufen
			if (enemies == null) {
				CreateSecondQuest(p);
			}
		}
		else if (qFlowerRed.IsDone() != qFlowerBlue.IsDone()) {
			// Sonst wird diese Nachricht ausgelöst nachem die erste Aufgabe erledigt wurde.
			p.lastTalk = "You have not yet picked both flowers";
		}
	}

	public void InitState(Player p)
	{
		CreateDummyQuest(p);
		flowerBlue.SetActive(true);
		flowerRed.SetActive(true);
	}

	public void LoadState(GameData gameData, Player p)
	{
		int level = gameData.LoadInt("thethirdkindlevel",0);
		flowerBlue.SetActive(true);
		flowerRed.SetActive(true);
		CreateDummyQuest(p);

		if ( level == 0 ) {
			// setup;
		}
		else {
			// mindestens level 1
			p.GetActiveQuest("TheThirdKind").Done();

			CreateFirstQuest(p);
			Quest qFlowerRed = p.GetQuest("FlowerRed");
			Quest qFlowerBlue = p.GetQuest("FlowerBlue");

			if ( level == 2 ) {
				qFlowerRed.Done();
				flowerRed.SetActive(false);
			}
			else if ( level == 3 ) {
				qFlowerBlue.Done();
				flowerBlue.SetActive(false);
			}
			else if ( level >= 4 ) {
				flowerBlue.SetActive(false);
				flowerRed.SetActive(false);
				qFlowerRed.Done();
				qFlowerBlue.Done();
				if ( level == 5 ) {
					p.GetActiveQuest("TheThirdKind").Done();
					CreateSecondQuest(p);
					FreedomQuest.npcsAlive = gameData.LoadInt("npcsalive",
						FreedomQuest.npcsCreated);
				}
			}
		}
	}

	public void SaveState(GameData gameData, Player p)
	{
		Quest qFlowerRed = p.GetQuest("FlowerRed");
		Quest qFlowerBlue = p.GetQuest("FlowerBlue");

		int level = 0;
		if (qFlowerRed == null) {
			// level ist 0
		}
		else if (qFlowerRed.IsDone() && qFlowerBlue.IsDone()) {
			Quest enemies = p.GetQuest("A1");
			if ( enemies != null )
				level = 5;
			else
				level = 4;
		}
		else {
			if (qFlowerRed.IsDone() != qFlowerBlue.IsDone()) {
				if ( qFlowerRed.IsDone() ) 
					level = 2;
				else if ( qFlowerBlue.IsDone() )
					level = 3;
			}
			else
				level = 1;
		}
		gameData.SaveInt("thethirdkindlevel",level);
		gameData.SaveInt("npcsalive",FreedomQuest.npcsAlive);
	}
}
