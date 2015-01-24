using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientCommunicator : MonoBehaviour {
	
	public int connectionPort = 25001;

	Dictionary<string, string> connectedAddrs;
	List<string> unusedAttacks;
	List<string> loadedAttacks;

	// Use this for initialization
	void Start () {
		Network.InitializeServer(32, connectionPort, false);

		connectedAddrs = new Dictionary<string, string>();
		unusedAttacks = new List<string>();
		loadedAttacks = new List<string>();

		unusedAttacks.Add("Fire");
		unusedAttacks.Add("Wind");
		unusedAttacks.Add("Water");
		unusedAttacks.Add("Marmot");
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
	
	void OnPlayerConnected(NetworkPlayer player)
	{
		int index = (int) Random.value * unusedAttacks.Count;
		string attack = unusedAttacks[index];

		loadedAttacks.Add(attack);
		unusedAttacks.RemoveAt(index);
		connectedAddrs.Add(player.ipAddress, attack);

		networkView.RPC("AssignClientAttack", player, attack);
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		string attack = connectedAddrs[player.ipAddress];

		connectedAddrs.Remove(player.ipAddress);
		unusedAttacks.Add(attack);
		loadedAttacks.Remove(attack);
	}

	[RPC]
	void RecvClientEvent(string attack)
	{
		// Get an event from a client.
		Debug.Log("Client attack! " + attack);
	}

	[RPC]
	void AssignClientAttack(string attack)
	{
		// Empty. Implemented on the client.
	}
}

