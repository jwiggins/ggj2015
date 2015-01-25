using UnityEngine;
using System.Collections;

public class Player : Mobile {

	bool canJump = true;
	bool hasJumped = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Jump")) {
			if (canJump && !hasJumped) {
				StartCoroutine("Jump");
				canJump = false;
				hasJumped = true;
			}
		}
		if (Input.GetButtonUp ("Jump")){ hasJumped = false; }
	}

	IEnumerator Jump () {
		rigidbody.velocity = Vector3.up * 9;
		yield return new WaitForSeconds(1f);
		canJump = true;
	}

	protected virtual void OnCollisionEnter (Collision collision) {
		if (gameObject.tag == "Unhittable") {
			Physics.IgnoreCollision(collision.gameObject.collider, gameObject.collider);
		}
	}
}
