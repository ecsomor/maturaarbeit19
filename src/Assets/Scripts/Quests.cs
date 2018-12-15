using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
	private List<Quest> questlist;

	public Text Questtext;

	// Use this for initialization
	void Start ()
	{
		questlist = new List<Quest> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool everythingDone = true;

		foreach (Quest q in questlist) {
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

	public void Interacted (GameObject obj)
	{
		Quest q = GetActiveQuest (obj.name);

		if (q != null) {
			if (q.Done ())
				Questtext.text = "";
		}
	}

	// adds a new quest to the list
	public void AddQuest (Quest q)
	{
		questlist.Add (q);
	}

	// searches for a quest by name
	public Quest GetQuest (string name)
	{
		foreach (Quest q in questlist) {
			if (name.StartsWith (q.Name ())) {
				return q;
			}
		}
		return null;
	}

	// searches for a still active quest by name
	public Quest GetActiveQuest (string name)
	{
		foreach (Quest q in questlist) {
			if (!q.IsDone () && name.StartsWith (q.Name ())) {
				return q;
			}
		}
		return null;
	}
}