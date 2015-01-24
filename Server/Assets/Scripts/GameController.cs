using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	int playerLives = 9;
	bool playerExists;
	GameObject playerObject;
	public GameObject playerObjectPrefab;

	bool spawningIsEnabled = true;

	List<GameObject> allMobiles;

	void Awake () {
		allMobiles = new List<GameObject>();
		playerObject = (GameObject) Instantiate(playerObjectPrefab, new Vector3 (-7.5f, 4f), Quaternion.identity);
		allMobiles.Add (playerObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator DestroyPlayer(){
		DestroyPlayerInternal ();
		yield return new WaitForSeconds (2);
		RespawnPlayer ();
	}

	void DestroyPlayerInternal (){
		Destroy (playerObject);
		playerLives -= 1;
		//DestroyAllMobiles();
		spawningIsEnabled = false;
	}

	void RespawnPlayer () {
		spawningIsEnabled = true;
		playerObject = (GameObject) Instantiate(playerObjectPrefab, new Vector3 (-7.5f, 0f), Quaternion.identity);
	}

	void DestroyAllMobiles () {
		//foreach (GameObject thisObject
	}
}
