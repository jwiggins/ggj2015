using UnityEngine;
using System.Collections;

public class NarutoSound : Sound
{
	protected override void Awake()
	{
		pause = 2.0f;
		base.Awake();
	}
}
