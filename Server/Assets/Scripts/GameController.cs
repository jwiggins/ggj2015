using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	int playerLives = 9;
	bool playerExists;
	GameObject playerObject;
	public GameObject playerPrefab;
	//NetworkThingy networker;

	static bool _spawningIsEnabled = true;
	public static bool spawningIsEnabled {
		get { return _spawningIsEnabled; }
		set { _spawningIsEnabled = value; }
	}

	List<GameObject> allMobiles;

	void Awake () {
		allMobiles = new List<GameObject>();
		//networker = gameObject.GetComponent<NetworkThingy>()
		playerObject = (GameObject) Instantiate(playerPrefab, new Vector3 (-7.5f, 4f), Quaternion.identity);
		allMobiles.Add (playerObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearField () {
		StartCoroutine (DestroyPlayer ());
	}

	IEnumerator DestroyPlayer(){
		DestroyAllMobiles ();
		playerLives -= 1;
		spawningIsEnabled = false;
		yield return new WaitForSeconds (2);
		spawningIsEnabled = true;
		playerObject = (GameObject) Instantiate(playerPrefab, new Vector3 (-7.5f, 0f), Quaternion.identity);
		allMobiles.Add (playerObject);
	}

	void DestroyAllMobiles () {
		foreach (GameObject thisObject in allMobiles) {
			Destroy (thisObject);
		}
		allMobiles.Clear ();
	}

	public void RemoveFromList (GameObject remObj) {
		if (allMobiles.Contains (remObj)) {
			allMobiles.Remove (remObj);
		} else {
			Debug.LogError ("Requested to remove object that is not on allMobiles list.");
		}
	}

	public void addToList (GameObject addObj) {
		allMobiles.Add (addObj);
	}
}
