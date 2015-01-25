using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communicator : MonoBehaviour {

    public int connectionPort = 25001;

	bool initialized = false;
	bool canAttack = true;
	string attackType;
	float attackPause = 1.0f;
	AudioSource attackSound;
	Dictionary<string, AudioSource> sources;
	Dictionary<string, float> pauses;

	void Awake()
	{
		Debug.Log("Communicator awakening!");
		Communicator maybeThis = GameObject.FindObjectOfType<Communicator>();

		if (!maybeThis.initialized)
		{
			// Keep this object around forever
			DontDestroyOnLoad(gameObject);

			sources = new Dictionary<string, AudioSource>();
			pauses = new Dictionary<string, float>();

			foreach (Object res in  Resources.LoadAll("Sounds")) {
				GameObject soundPrefab = (GameObject) res;
				
				GameObject soundInstance = (GameObject) Instantiate(soundPrefab, transform.position, Quaternion.identity);
				AudioSource source = soundInstance.GetComponent<AudioSource>();
				Sound sound = soundInstance.GetComponent<Sound>();

				DontDestroyOnLoad(source);
				sources.Add(soundPrefab.tag, source);
				pauses.Add(soundPrefab.tag, sound.pause);
			}

			initialized = true;
		}
	}

	public void Connect(string address)
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			Network.Connect(address, connectionPort);
		}
	}

	public void Attack()
	{
		if (Network.peerType == NetworkPeerType.Client && canAttack)
		{
			attackSound.PlayOneShot(attackSound.clip);
			networkView.RPC("RecvClientEvent", RPCMode.Server, attackType);
			canAttack = false;
			StartCoroutine("WeaponDelay");
		}
	}

	IEnumerator WeaponDelay() {
		yield return new WaitForSeconds(attackPause);
		canAttack = true;
	}

	[RPC]
	void AssignClientAttack(string attack)
	{
		attackType = attack;
		attackSound = sources[attackType];
		attackPause = pauses[attackType];
	}

	[RPC]
	void RecvClientEvent(string attack)
	{
		// Empty. Implemented on the server.
	}
}