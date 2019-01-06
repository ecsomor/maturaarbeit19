using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdKind : InteractibleObject
{
	private Quest bringFlowers;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void Interact(Player p)
	{

		Quest qFlowerRed = p.GetQuest("FlowerRed");
		Quest qFlowerBlue = p.GetQuest("FlowerBlue");

		if (qFlowerRed == null) {
			qFlowerRed = new Quest("FlowerRed", "Go and pick the flower behind the house");
			qFlowerBlue = new Quest("FlowerBlue", "Go and pick the flower at the corner");
			p.AddQuest(qFlowerRed);
			p.AddQuest(qFlowerBlue);

			bringFlowers = new Quest("TheThirdKind", "You must come back to the wiser green man");
			p.AddQuest(bringFlowers);
			p.lastTalk = "You must pick the two flowers and come back to me. The picked flowers give you regeneration points";
		}
		else if (qFlowerRed.IsDone() && qFlowerBlue.IsDone()) {
			// Beim ersten mal zurückkommen wenn die Aufgabe noch nicht erledigt ist - gib Geld einmal
			if (bringFlowers.IsDone() == false) {
				p.AddQuest(new FreedomQuest("You must fight all red figures"));

				//p.AddQuest (new Quest ("AI1", "Fight the red figures"));
				// p.AddQuest (new Quest ("AI2", "Fight the red figures"));
				p.lastTalk = "Great, now you must fight the red figures. Pick the flowers you find to get regeneration points";
			}
		}
		else if (qFlowerRed.IsDone() != qFlowerBlue.IsDone()) {
			// Sonst wird diese Nachricht ausgelöst nachem die erste Aufgabe erledigt wurde.
			p.lastTalk = "You have not yet picked both flowers";
		}
	}
}
