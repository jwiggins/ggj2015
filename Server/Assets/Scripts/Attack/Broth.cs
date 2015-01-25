using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Broth : Attack {

	int myBrothIndex = 0;
	const int maxBroth = 5;
	const float brothTime = 0.1f;
	Broth prev;
	Broth next;
	public Sprite topper;
	public GameObject brothPrefab;

	List<GameObject> thisTower;

	public override void DeclareMyProperties () {
		myName = "Broth";
		myReloadTime = 6.0f;
		base.DeclareMyProperties ();
	}

	protected override void Awake () {
		thisTower = new List<GameObject> ();
		thisTower.Add (gameObject);
	}

	// Use this for initialization
	protected override void Start () {

	}
	
	// Update is called once per frame
	protected override void Update () {
		
	}

	public override void Spawn () {
		float spawnXPos = Random.Range (-4.5f, 8f);
		transform.position = new Vector2 (spawnXPos, -4.5f);
		StartCoroutine (BrothSpawn());
	}

	public void connectBrothIndex (int index, Broth previous, List<GameObject> tower) {
		myBrothIndex = index;
		prev = previous;
		transform.position = new Vector2 (prev.transform.position.x, prev.transform.position.y + 1f);
		thisTower = tower;
	}

	public IEnumerator BrothSpawn () {
		thisTower.Add (gameObject);
		if (myBrothIndex == maxBroth) {
			gameObject.GetComponent<SpriteRenderer>().sprite = topper;
			yield return new WaitForSeconds(brothTime * 2);
			KillMe ();
		}
		yield return new WaitForSeconds(brothTime);
		GameObject tempObject = (GameObject) Instantiate (brothPrefab, 
		                                                  new Vector3(transform.position.x, transform.position.y + 1f, 0),
		                                                  Quaternion.identity);
		next = tempObject.GetComponent<Broth>();
		next.connectBrothIndex (myBrothIndex + 1, this, thisTower);
		StartCoroutine (next.BrothSpawn());
	}

	public void KillMe() {
		foreach(GameObject broth in thisTower){
			Destroy (broth);
		}
	}
}
