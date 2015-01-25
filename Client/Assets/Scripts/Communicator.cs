using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Communicator : MonoBehaviour {

    public int connectionPort = 25001;

	string connectionIP = "";
	string attackType;
	AudioSource attackSound;
	Dictionary<string, AudioSource> sounds;

	void Start()
	{
		sounds = new Dictionary<string, AudioSource>();

		foreach (Object res in  Resources.LoadAll ("Sounds")) {
			GameObject soundPrefab = (GameObject) res;
			GameObject soundInstance = (GameObject) Instantiate(soundPrefab, transform.position, Quaternion.identity);
			AudioSource sound = soundInstance.GetComponent<AudioSource>();
			sounds.Add(soundPrefab.tag, sound);
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
		if (Network.peerType == NetworkPeerType.Client)
		{
			Debug.Log("Invoking RecvClientEvent on the server.");
			attackSound.PlayOneShot(attackSound.clip);
			networkView.RPC("RecvClientEvent", RPCMode.Server, attackType);
		}
	}

	[RPC]
	void AssignClientAttack(string attack)
	{
		attackType = attack;

		attackSound = sounds[attack];
		Debug.Log("My attack is:" + attack);
	}

	[RPC]
	void RecvClientEvent(string attack)
	{
		// Empty. Implemented on the server.
	}
}