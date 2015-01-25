using UnityEngine;
using System.Collections;

public class Evade : Attack {

	GameObject playerObject;
	public Sprite duck;
	Sprite playerOriginal;

	public override void DeclareMyProperties () {
		myName = "Evade";
		myReloadTime = 5.0f;
		base.DeclareMyProperties ();
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
		playerObject = GameObject.Find("GameController").GetComponent<GameController>().playerObject;
 		playerObject.tag = "Unhittable";
		playerObject.layer = 13;
		SpriteRenderer playerRenderer = playerObject.GetComponent<SpriteRenderer> ();
		playerOriginal = playerRenderer.sprite;
		playerObject.GetComponent<SpriteRenderer>().sprite = duck;
		yield return new WaitForSeconds(2);
		playerObject.tag = "Player";
		playerObject.layer = 10;
		playerObject.GetComponent<SpriteRenderer>().sprite = playerOriginal;
		GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(this.gameObject);
		Destroy (this.gameObject);
	}
}