using UnityEngine;
using System.Collections;

public class ChopSound : Sound
{
	protected override void Awake()
	{
		pause = 0.3f;
		base.Awake();
	}
}
