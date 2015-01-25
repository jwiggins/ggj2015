using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowEnemy : Enemy {

	
	float count = 0.0f;
	float height = 6.0f;
	float speed = -3.0f;
	float timingOffset = 0.0f;

	protected override void Awake () {
		spawnChance = 0.05f;
		availableSpawners = new List<int> {1,2,6,7};
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		count += Time.deltaTime;
		float offset = Mathf.Sin(Time.time * speed + timingOffset) * height / 2;
		transform.position = new Vector3(transform.position.x - count / 10, offset, 0);
		base.Update ();
	}

	protected override void Move (){}
}
