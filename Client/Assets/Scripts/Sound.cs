using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Sound : MonoBehaviour
{
	float _pause = 1.0f;

	public float pause
	{
		get { return _pause; }
		set { _pause = value; }
	}

	protected virtual void Awake()
	{
	}
}
