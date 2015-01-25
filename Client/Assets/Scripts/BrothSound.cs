using UnityEngine;
using System.Collections;

public class BrothSound : Sound
{
	protected override void Awake()
	{
		pause = 4.0f;
		base.Awake();
	}
}
