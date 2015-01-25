using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communicator : MonoBehaviour {
	
    public int connectionPort = 25001;
	string connectionAddress = "";
	bool doneTesting = false;

	bool initialized = false;
	bool canAttack = true;
	string attackType;
	float attackPause = 1.0f;
	AudioSource attackSound;
	Dictionary<string, AudioSource> sources;
	Dictionary<string, float> pauses;

	void CheckNetwork()
	{
		ConnectionTesterStatus status = Network.TestConnection();
		if (status == ConnectionTesterStatus.Undetermined)
			return;

		switch (status)
		{
		case ConnectionTesterStatus.Error:
			Debug.Log("Network test: Error");
			break;
		case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
			Debug.Log("Network test: LimitedNATPunchthroughPortRestricted");
			break;
		case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
			Debug.Log("Network test: LimitedNATPunchthroughSymmetric");
			break;
		case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
			Debug.Log("Network test: NATpunchthroughAddressRestrictedCone");
			break;
		case ConnectionTesterStatus.NATpunchthroughFullCone:
			Debug.Log("Network test: NATpunchthroughFullCone");
			break;
		case ConnectionTesterStatus.PublicIPIsConnectable:
			Debug.Log("Network test: PublicIPIsConnectable");
			break;
		case ConnectionTesterStatus.PublicIPNoServerStarted:
			Debug.Log("Network test: PublicIPNoServerStarted");
			break;
		case ConnectionTesterStatus.PublicIPPortBlocked:
			Debug.Log("Network test: PublicIPPortBlocked");
			break;
		case ConnectionTesterStatus.Undetermined:
			Debug.Log("Network test: Undetermined");
			break;
		}

		doneTesting = true;
	}


	void Update()
	{
		if (!doneTesting)
			CheckNetwork();
	}

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


	public void Reconnect()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			Network.Connect(connectionAddress, connectionPort);
		}
		else if (Network.peerType == NetworkPeerType.Client)
		{
			Network.Disconnect(200);
			Network.Connect(connectionAddress, connectionPort);
		}
	}


	public void Connect(string address)
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			Network.Connect(address, connectionPort);
			connectionAddress = address;
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
		Debug.Log("Got the " + attack + " attack from the server!");
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