using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	int playerLives = 9;
	bool playerExists;
	GameObject _playerObject;
	public GameObject playerObject {
		get { return _playerObject; }
		set { _playerObject = value; }
	}
	public GameObject playerPrefab;

	public Text livesText;
	public GameObject deathImage;

	List<Enemy> enemies = new List<Enemy> ();

	List<Spawner> spawners;
	public GameObject spawnerPrefab;

	ClientCommunicator cComm;

	static bool _spawningIsEnabled = true;
	public static bool spawningIsEnabled {
		get { return _spawningIsEnabled; }
		set { _spawningIsEnabled = value; }
	}

	List<GameObject> allMobiles;

	void Awake () {
		allMobiles = new List<GameObject>();
		cComm = gameObject.GetComponent<ClientCommunicator> ();
		playerObject = (GameObject) Instantiate(playerPrefab, new Vector3 (-7.5f, 4f), Quaternion.identity);
		allMobiles.Add (playerObject);
		spawners = new List<Spawner> ();
		for (int i = 0; i <= 8; i++) {
			GameObject newSpawner = (GameObject) Instantiate(spawnerPrefab, new Vector2 (11, 4 - i), 
			                                                 Quaternion.identity);
			Spawner sScript = newSpawner.GetComponent<Spawner>();
			spawners.Add(sScript);
			sScript.spawnerIndex = i;
		}
		foreach (Object i in  Resources.LoadAll ("Enemies")) {
			GameObject enemyPrefab = (GameObject) i;
			Enemy enemy = enemyPrefab.GetComponent<Enemy>();
			enemies.Add(enemy);
			foreach (Spawner j in spawners) {
				if (enemy.getAvailableSpawners().Contains(j.spawnerIndex)){
					j.SetSpawnableEnemy(enemy);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		livesText = GameObject.Find ("NineLivesGui").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearField () {
		StartCoroutine (DestroyPlayer ());
	}

	IEnumerator DestroyPlayer(){
		if (spawningIsEnabled){
			DestroyAllMobiles ();
			playerLives -= 1;
			livesText.text = playerLives.ToString ();
			spawningIsEnabled = false;
			if (playerLives > 0) {
				yield return new WaitForSeconds (2);
				spawningIsEnabled = true;
 				playerObject = (GameObject) Instantiate(playerPrefab, new Vector3 (-7.5f, 0f), Quaternion.identity);
				allMobiles.Add (playerObject);
			} else {
				GameObject death = (GameObject)Instantiate (deathImage);
				death.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				yield return new WaitForSeconds (5);
				spawningIsEnabled = true;
				Application.LoadLevel("OpeningScene");
			}
		}
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

	public void procAttack (string name) {
		if (spawningIsEnabled){
			GameObject thisAttack = (GameObject) Instantiate(Resources.Load ("Attack Prefabs/" + name),
			                                                 playerObject.transform.position, Quaternion.identity);
			thisAttack.GetComponent<Attack>().Spawn();
			addToList (thisAttack);
		}
	}

	public void oneUp () {
		playerLives += 1;
		livesText.text = playerLives.ToString ();
	}
}
