using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
	// a quest with the name of the target object and a task description string
	// quests that must be done previously are given as optional parameter
	public Quest (string nameString, string taskString, Quest[] preconditions = null)
	{
		name = nameString;
		task = taskString;
		done = false;
		precond = preconditions;
	}

	// determines whether all preconditions are ok
	public bool CanBeDone ()
	{
		if (precond != null) {
			foreach (Quest q in precond) {
				if (q.IsDone () == false)
					return false;
			}
		}
		return true;
	}

	public bool IsDone ()
	{
		return done;
	}

	// attempt at doing this task, test precondition, return real state
	public bool Done ()
	{
		if (CanBeDone ())
			done = true;

		return IsDone ();
	}

	public string Name ()
	{
		return name;
	}

	public string Task ()
	{
		return task;
	}

	private string name;
	private string task;
	private bool done;
	private Quest[] precond;
};
