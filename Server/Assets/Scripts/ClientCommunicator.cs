using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientCommunicator : MonoBehaviour {
	
	public int connectionPort = 25001;
	public List<string> connectedAddrs;

	// Use this for initialization
	void Start () {
		Network.InitializeServer(32, connectionPort, false);
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
			if (GUI.Button(new Rect(10, 50, 120, 20), "Disconnect"))
			{
				Network.Disconnect(200);
			}
			string allClients = "Clients: ";
			foreach (string playerIp in connectedAddrs)
			{
				allClients = allClients + playerIp;
			}
			GUI.Label(new Rect(10, 70, 300, 20), allClients);
		}
	}
	
	void OnPlayerConnected(NetworkPlayer player)
	{
		connectedAddrs.Add(player.ipAddress);
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		connectedAddrs.Remove(player.ipAddress);
	}

	[RPC]
	void RecvClientMessage(string msg)
	{
	}
}