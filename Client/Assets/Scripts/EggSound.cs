using UnityEngine;
using System.Collections;

public class EggSound : Sound
{
	protected override void Awake()
	{
		pause = 2.5f;
		base.Awake();
	}
}
