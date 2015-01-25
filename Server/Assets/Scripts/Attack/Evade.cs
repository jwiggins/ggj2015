using UnityEngine;
using System.Collections;

public class Evade : Attack {

	GameObject playerObject;

	public override void DeclareMyName () {
		myName = "Evade";
		base.DeclareMyName ();
	}
	
	protected override void Awake () {
		playerObject = GameObject.Find ("Player");
		base.Awake ();
	}
	
	// Use this for initialization
	protected override void Start () {
		StartCoroutine (Invulnerability ());
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
	
	protected override void OnCollisionEnter (Collision collision) {
	}

	IEnumerator Invulnerability () {
		playerObject.tag = "Unhittable";
		playerObject.layer = 13;
		yield return new WaitForSeconds(3);
		playerObject.tag = "Player";
		playerObject.layer = 10;
		GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(this.gameObject);
		Destroy (this.gameObject);
	}
}