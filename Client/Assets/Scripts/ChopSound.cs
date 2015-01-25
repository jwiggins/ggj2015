using UnityEngine;
using System.Collections;

public class ChopSound : Sound
{
	protected override void Awake()
	{
		pause = 1.0f;
		base.Awake();
	}
}
