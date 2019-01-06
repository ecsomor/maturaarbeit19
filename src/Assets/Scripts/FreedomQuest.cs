using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreedomQuest : Quest
{
	public static int npcsAlive;

	public FreedomQuest(string taskString) : base("A1", taskString)
	{
	}

	public override bool IsDone()
	{
		return npcsAlive < 1;
	}

	public override string Task()
	{
		return "Fight the remaining\n" + npcsAlive + " red figures";
	}
}
