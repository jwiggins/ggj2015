using UnityEngine;
using System.Collections;

public class EvadeSound : Sound
{
	protected override void Awake()
	{
		pause = 10.0f;
		base.Awake();
	}
}

