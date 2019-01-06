using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
	private string name;
	private string task;
	private bool done;

	// Eine Aufgabe mit dem namen des Zielobjekts und einer Aufgabenbeschreibung.
	public Quest(string nameString, string taskString)
	{
		name = nameString;
		task = taskString;
		done = false;
	}

	virtual public bool IsDone()
	{
		return done;
	}

	public bool Done()
	{
		done = true;
		return IsDone();
	}

	public string Name()
	{
		return name;
	}

	virtual public string Task()
	{
		return task;
	}
};
