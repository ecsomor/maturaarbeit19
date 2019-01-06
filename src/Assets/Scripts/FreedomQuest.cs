using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreedomQuest : Quest
{
	public static int npcsCreated;
	public static int npcsAlive;

	public FreedomQuest(string taskString) : base("A1", taskString)
	{
		npcsAlive = npcsCreated;
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
