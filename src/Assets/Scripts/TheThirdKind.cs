using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThirdKind : InteractibleObject
{
	private Quest bringFlowers;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void Interact (Player p)
	{

		Quest qFlowerRed = p.GetQuest ("FlowerRed");
		Quest qFlowerBlue = p.GetQuest ("FlowerBlue");

		if (qFlowerRed == null) {
			qFlowerRed = new Quest ("FlowerRed", "Go and pick the flower behind the house");
			qFlowerBlue = new Quest ("FlowerBlue", "Go and pick the flower at the corner");
			p.AddQuest (qFlowerRed);
			p.AddQuest (qFlowerBlue);

			bringFlowers = new Quest ("TheThirdKind", "You must bring the flowers to the wiser green man", new Quest[] {
				qFlowerRed,
				qFlowerBlue
			});
			p.AddQuest (bringFlowers);
			p.lastTalk = "You must pick the two flowers and bring them to me";
		} else if (qFlowerRed.IsDone () && qFlowerBlue.IsDone ()) {
			// the first time coming back, task is not yet done - give money once
			if (bringFlowers.IsDone () == false) {
				p.money += 30;
				p.AddQuest (new Quest ("AI1", "Fight the red figures"));
				p.AddQuest (new Quest ("AI2", "Fight the red figures"));
				p.lastTalk = "Thank you, now you must fight the red figures";
			}
		} else if (qFlowerRed.IsDone () != qFlowerBlue.IsDone ()) {
			// otherwise this message is triggered immediately after getting the first quest
			p.lastTalk = "You have not yet picked both flowers";
		}
	}
}
