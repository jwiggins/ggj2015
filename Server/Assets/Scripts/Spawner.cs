using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject spawnableEnemy;
	bool waitingToSpawn = true;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnChecker ());
	}
	
	// Update is called once per frame
	void Update () {
		if (!waitingToSpawn) {
			StartCoroutine (SpawnChecker ());
			waitingToSpawn = true;
		}
	}

	IEnumerator SpawnChecker () {
		yield return new WaitForSeconds (1);
		float spawnRandom = Random.Range (0.0f, 1.0f);
		if (spawnRandom > 0.96f && GameController.spawningIsEnabled) {
			GameObject thisEnemy = (GameObject) GameObject.Instantiate(spawnableEnemy, 
			                                    gameObject.transform.position, Quaternion.identity);
			GameObject.Find("GameController").GetComponent<GameController>().addToList(thisEnemy);
		}
		waitingToSpawn = false;
	}
}
