using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public int spawnerIndex;
	public GameObject spawnableEnemy;
	bool waitingToSpawn = true;
	List<Enemy> spawnables = new List<Enemy>();
	List<float> maxChances = new List<float> ();

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
		GameObject thisEnemy = determineSpawnChoice(Random.Range (0.0f, 1.0f));
		if (thisEnemy != null && GameController.spawningIsEnabled) {
			thisEnemy = (GameObject) GameObject.Instantiate(thisEnemy, 
			                                    gameObject.transform.position, Quaternion.identity);
			GameObject.Find("GameController").GetComponent<GameController>().addToList(thisEnemy);
		}
		waitingToSpawn = false;
	}

	GameObject determineSpawnChoice(float perc) {
		int iterator = 0;
		foreach (float i  in maxChances) {
			if (perc < maxChances[iterator]) {
				return spawnables[iterator].gameObject;
			}
			iterator++;
		}
		return null;
	}

	public void SetSpawnableEnemy (Enemy thisEnemy){
		spawnables.Add (thisEnemy);

		float maxChance = thisEnemy.spawnChance;
		if (maxChances.Count > 0) {
			maxChance += maxChances[maxChances.Count - 1];
		}
		maxChances.Add(maxChance);
	}
}
