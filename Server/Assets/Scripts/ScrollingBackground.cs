using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {

	public GameObject nextBackground;
	bool hasSpawnedNext = false;

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(-3f,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -20.0f && !hasSpawnedNext) {
			Instantiate (nextBackground, new Vector3(40, -2, 0), Quaternion.identity);
			hasSpawnedNext = true;
		} else if (transform.position.x < -40.0f) {
			Destroy (this.gameObject);
		}
	}
}
