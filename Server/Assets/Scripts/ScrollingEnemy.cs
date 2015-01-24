using UnityEngine;
using System.Collections;

public class ScrollingEnemy : Mobile {

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(-5f,0,0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			StartCoroutine(GameObject.Find("GameController").GetComponent<GameController>().DestroyPlayer());
		}
	}
}
