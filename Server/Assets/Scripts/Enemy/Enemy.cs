using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : Mobile {

	protected List<int> availableSpawners = new List<int> {0,1,2,3,4,5,6,7,8};
	float _spawnChance = 0.05f;
	public float spawnChance {
		get { return _spawnChance; }
		set { _spawnChance = value; }
	}

	protected virtual void Awake () {
	}

	// Use this for initialization
	protected virtual void Start () {
		Move ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (transform.position.x < -15.0f) {
			GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(gameObject);
			GameObject.Destroy(gameObject);
		}
	}

	protected void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			GameObject.Find("GameController").GetComponent<GameController>().ClearField();
		}
	}

	public virtual List<int> getAvailableSpawners() {
		return availableSpawners;
	}

	protected virtual void Move() {
		rigidbody.velocity = new Vector3(-5f,0,0);
	}
}
