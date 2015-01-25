using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communicator : MonoBehaviour {

    public int connectionPort = 25001;

	string connectionIP = "";
	bool canAttack = true;
	string attackType;
	float attackPause = 1.0f;
	AudioSource attackSound;
	Dictionary<string, AudioSource> sources;
	Dictionary<string, float> pauses;

	void Start()
	{
		sources = new Dictionary<string, AudioSource>();
		pauses = new Dictionary<string, float>();
		foreach (Object res in  Resources.LoadAll("Sounds")) {
			GameObject soundPrefab = (GameObject) res;
			
			GameObject soundInstance = (GameObject) Instantiate(soundPrefab, transform.position, Quaternion.identity);
			AudioSource source = soundInstance.GetComponent<AudioSource>();
			Sound sound = soundInstance.GetComponent<Sound>();
			sources.Add(soundPrefab.tag, source);
			pauses.Add(soundPrefab.tag, sound.pause);
		}
	}
	
    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
            connectionIP = GUI.TextField(new Rect(10, 30, 300, 50), connectionIP);
            if (GUI.Button(new Rect(10, 100, 120, 50), "Client Connect"))
            {
                Network.Connect(connectionIP, connectionPort);
            }
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client");
            if (GUI.Button(new Rect(10, 30, 120, 50), "Disconnect"))
            {
                Network.Disconnect(200);
            }
		}
    }

	public void Attack()
	{
		if (Network.peerType == NetworkPeerType.Client && canAttack)
		{
			Debug.Log("Invoking RecvClientEvent on the server.");
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

		attackSound = sources[attack];
		attackPause = pauses[attack];
		Debug.Log("My attack is:" + attack);
	}

	[RPC]
	void RecvClientEvent(string attack)
	{
		// Empty. Implemented on the server.
	}
}