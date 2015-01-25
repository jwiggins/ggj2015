using UnityEngine;
using System.Collections;

public class EvadeSound : Sound
{
	protected override void Awake()
	{
		pause = 5.0f;
		base.Awake();
	}
}

