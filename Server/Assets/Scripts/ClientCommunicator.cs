using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientCommunicator : MonoBehaviour {
	
	public int connectionPort = 25001;

	Dictionary<string, string> connectedAddrs;
	List<string> unusedAttacks;
	List<string> loadedAttacks;
	Dictionary<string, float> attackPauses;
	List<NetworkPlayer> clients;

	GameController controller;

	// Use this for initialization
	void Awake () {
		Network.InitializeServer(32, connectionPort, false);

		connectedAddrs = new Dictionary<string, string>();
		unusedAttacks = new List<string>();
		loadedAttacks = new List<string>();
		attackPauses = new Dictionary<string, float>();

		clients = new List<NetworkPlayer> ();
	}

	void Start () {
		foreach (Object i in  Resources.LoadAll ("Attack Prefabs")) {
			GameObject attackPrefab = (GameObject) i;
			Attack attack = attackPrefab.GetComponent<Attack>();
			unusedAttacks.Add(attack.myName);
			attackPauses[attack.myName] = attack.myReloadTime;
		}
		controller = gameObject.GetComponent<GameController> ();

	}

	void OnGUI()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			string ipAddr = Network.player.ipAddress;
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Server");
			GUI.Label(new Rect(10, 30, 300, 20), ipAddr);
			string allClients = "Clients: ";
			foreach (string playerIp in connectedAddrs.Keys)
			{
				allClients = allClients + playerIp;
			}
			GUI.Label(new Rect(10, 50, 300, 20), allClients);
		}
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		string attack = assignRandomAbility (player);
		clients.Add (player);
		connectedAddrs.Add (player.ipAddress, attack);
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		string attack = connectedAddrs[player.ipAddress];

		connectedAddrs.Remove(player.ipAddress);
		unusedAttacks.Add(attack);
		loadedAttacks.Remove(attack);
	}

	public void randomizeAllClients() {
		foreach (string attack in loadedAttacks) {
			unusedAttacks.Add(attack);
			loadedAttacks.Remove(attack);
		}
		foreach (NetworkPlayer client in clients) {
			assignRandomAbility(client);
		}
	}

	string assignRandomAbility(NetworkPlayer player) {
		int index = (int) (Random.value * unusedAttacks.Count);
		string attack = unusedAttacks[index];
		float pause = attackPauses[attack];
		
		loadedAttacks.Add(attack);
		unusedAttacks.RemoveAt(index);

		networkView.RPC("AssignClientAttack", player, attack, pause);
		return attack;
	}

	[RPC]
	void RecvClientEvent(string attack) {
		// Get an event from a client.
		Debug.Log("Client attack! " + attack);

		controller.procAttack (attack);
	}

	[RPC]
	void AssignClientAttack(string attack, float pause) {
		// Empty. Implemented on the client.
	}
}