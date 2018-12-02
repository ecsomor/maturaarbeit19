using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class Quest
{
	
	public Quest( string nameString, string taskString )
	{
		name = nameString;
		task = taskString;
		done = false;
	}

	public bool IsDone()
	{
		return done;
	}

	public void Done()
	{
		done = true;
	}

	public string Name()
	{
		return name;
	}

	public string Task()
	{
		return task;
	}

	private string name;
	private string task;
	private bool done;
};

public class Quests : MonoBehaviour {
	private List<Quest> questlist;

	public Text Questtext;
	//public Text ActiveQuest;
	// private GameData Quest;

	// Use this for initialization
	void Start () {
		questlist = new List<Quest> ();
		InitQuests ();
	}
	
	// Update is called once per frame
	void Update () {
		bool everythingDone = true;

		foreach (Quest q in questlist )
		{
			if (!q.IsDone ()) {
				Questtext.text = q.Task ();
				everythingDone = false;
				break;
			}
		}

		if (everythingDone) {
			Questtext.text = "Mission accomplished";
		}
	}

	public void Interacted(GameObject obj)
	{
		foreach (Quest q in questlist )
		{
			if (!q.IsDone () && obj.name == q.Name()  ) {
				q.Done ();
				Questtext.text = "";
				break;
			}
		}

	}

	void InitQuests() {
		questlist.Add (new Quest ("Flower", "Go and pick the flower behind the house"));
		questlist.Add (new Quest ("FlowerBlue", "Go and pick the flower at the corner"));
	}
}