using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
	// Eine Aufgabe mit dem namen des Zielobjekts und einer Aufgabenbeschreibung.
	// Aufgaben die vorab erledigt werden müssen werden als optionale Parameter mitgegeben.
	public Quest (string nameString, string taskString, Quest[] preconditions = null)
	{
		name = nameString;
		task = taskString;
		done = false;
		precond = preconditions;
	}

	// Entscheidet ob alle Bedingungen erfüllt sind.
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

	// Teste ob alle Bedingungen erfüllt sind und gebe Status zurück.
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
