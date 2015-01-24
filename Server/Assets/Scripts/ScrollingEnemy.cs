using UnityEngine;
using System.Collections;

public class ScrollingEnemy : Mobile {

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(-5f,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -15.0f) {
			GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(gameObject);
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			GameObject.Find("GameController").GetComponent<GameController>().ClearField();
		}
	}
}
