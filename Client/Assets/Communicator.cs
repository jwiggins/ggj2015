﻿using UnityEngine;
using System.Collections;

public class Communicator : MonoBehaviour {

    public int connectionPort = 25001;

	string connectionIP = "";
	string attackType;

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
            connectionIP = GUI.TextField(new Rect(10, 30, 300, 20), connectionIP);
            if (GUI.Button(new Rect(10, 50, 120, 20), "Client Connect"))
            {
                Network.Connect(connectionIP, connectionPort);
            }
        }
        else if (Network.peerType == NetworkPeerType.Client)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client");
            if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
			if (GUI.Button(new Rect(10, 50, 120, 20), "Attack!"))
			{
				networkView.RPC("RecvClientEvent", RPCMode.Server, attackType);
			}
		}
    }

	[RPC]
	void AssignClientAttack(string attack)
	{
		attackType = attack;
		Debug.Log("My attack is:" + attack);
	}

	[RPC]
	void RecvClientEvent(string attack)
	{
		// Empty. Implemented on the server.
	}
}