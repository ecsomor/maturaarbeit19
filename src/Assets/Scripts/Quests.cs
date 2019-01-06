using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
	private List<Quest> questlist;

	public Text Questtext;


	void Start()
	{
		questlist = new List<Quest>();
	}

	void Update()
	{
		bool everythingDone = true;

		foreach (Quest q in questlist) {
			if (!q.IsDone()) {
				Questtext.text = q.Task();
				everythingDone = false;
				break;
			}
		}

		if (everythingDone) {
			Questtext.text = "Mission accomplished";
		}
	}

	public void Clear()
	{
		questlist.Clear();
	}

	public void Interacted(GameObject obj)
	{
		Quest q = GetActiveQuest(obj.name);

		if (q != null) {
			if (q.Done())
				Questtext.text = "";
		}
	}

	// fügt der Liste eine neue Aufgabe hinzu
	public void AddQuest(Quest q)
	{
		questlist.Add(q);
	}

	// Sucht eine Aufgabe nach Name
	public Quest GetQuest(string name)
	{
		foreach (Quest q in questlist) {
			if (name.StartsWith(q.Name())) {
				return q;
			}
		}
		return null;
	}

	// Sucht eine aktive Aufgabe nach Name
	public Quest GetActiveQuest(string name)
	{
		foreach (Quest q in questlist) {
			if (!q.IsDone() && name.StartsWith(q.Name())) {
				return q;
			}
		}
		return null;
	}
}