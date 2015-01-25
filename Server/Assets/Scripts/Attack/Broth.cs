using UnityEngine;
using System.Collections;

public class Broth : Attack {

	int myBrothIndex = 0;
	const int maxBroth = 5;
	const float brothTime = 0.1f;
	Broth prev;
	Broth next;
	public Sprite topper;
	public GameObject brothPrefab;

	public override void DeclareMyProperties () {
		myName = "Broth";
		myReloadTime = 6.0f;
		base.DeclareMyProperties ();
	}

	protected override void Awake () {

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

	public void connectBrothIndex (int index, Broth previous) {
		myBrothIndex = index;
		prev = previous;
		transform.position = new Vector2 (prev.transform.position.x, prev.transform.position.y + 1f);
	}

	public IEnumerator BrothSpawn () {
		if (myBrothIndex == maxBroth) {
			gameObject.GetComponent<SpriteRenderer>().sprite = topper;
			yield return new WaitForSeconds(brothTime);
			StartCoroutine(prev.BrothDestroy());
			Destroy (gameObject);
		}
		yield return new WaitForSeconds(brothTime);
		next = ((GameObject) Instantiate(brothPrefab)).GetComponent<Broth>();
		next.connectBrothIndex (myBrothIndex + 1, this);
		StartCoroutine (next.BrothSpawn());

	}

	public IEnumerator BrothDestroy () {
		yield return new WaitForSeconds(brothTime);
		if (myBrothIndex == 0) {
			GameObject.Find("GameController").GetComponent<GameController>().RemoveFromList(gameObject);
		} else {
			StartCoroutine (prev.BrothDestroy ());
		}
		Destroy (gameObject);
	}
}
