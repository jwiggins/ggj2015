using UnityEngine;
using System.Collections;

public class ForGreatJustice : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (waitAndJump ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator waitAndJump () {
		yield return new WaitForSeconds (5);
		Application.LoadLevel ("OpeningScene");
	}
}
