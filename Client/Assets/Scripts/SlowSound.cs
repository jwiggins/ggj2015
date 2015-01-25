using UnityEngine;
using System.Collections;

public class SlowSound : Sound
{
	protected override void Awake()
	{
		pause = 5.0f;
		base.Awake();
	}
}
