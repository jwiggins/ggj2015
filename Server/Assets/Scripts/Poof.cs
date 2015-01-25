using UnityEngine;
using System.Collections;

public class Poof : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (die ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator die () {
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
