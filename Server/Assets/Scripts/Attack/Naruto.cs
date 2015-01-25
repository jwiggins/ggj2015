using UnityEngine;
using System.Collections;

public class Naruto : Attack {

	public override void DeclareMyProperties () {
		myName = "Narutomaki";
		myReloadTime = 2.0f;
		base.DeclareMyProperties ();
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
