using UnityEngine;
using System.Collections;

public class Chopsticks : Attack {

	public override void DeclareMyName () {
		myName = "Chopsticks";
		base.DeclareMyName ();
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
		base.OnCollisionEnter (collision);
		if (collision.gameObject.tag == "Mobiles") {
			GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(this.gameObject);
			Destroy (this.gameObject);
		}
	}
}
