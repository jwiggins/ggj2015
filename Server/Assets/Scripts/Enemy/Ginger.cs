using UnityEngine;
using System.Collections;

public class Ginger : Enemy {

	public override void InitializeEnemy () {
		spawnChance = 0.005f;
	}

	protected override void Awake () {
		base.Awake ();
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.audio.PlayOneShot(collision.gameObject.audio.clip);
			GameObject.Find("GameController").GetComponent<ClientCommunicator>().randomizeAllClients();
			GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(this.gameObject);
			Destroy (this.gameObject);
		}
	}

	protected override void Move() {
		rigidbody.velocity = new Vector3(-8f,0,0);
	}
}