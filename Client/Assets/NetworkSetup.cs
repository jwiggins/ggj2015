using UnityEngine;
using System.Collections;

public class NetworkSetup : MonoBehaviour {

    public string connectionIP;
    public string sendText;
    public int connectionPort = 25001;

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
            string connectedIP = Network.player.externalIP;
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client");
			GUI.Label(new Rect(10, 30, 300, 20), "External Address:" + connectedIP);
            if (GUI.Button(new Rect(10, 50, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }

            sendText = GUI.TextField(new Rect(10, 80, 300, 20), sendText);
            if (GUI.Button(new Rect(10, 100, 120, 20), "Send text"))
            {
                networkView.RPC("RecvClientMessage", RPCMode.Server, sendText);
            }
        }
    }

    [RPC]
    void RecvClientMessage(string msg)
    {
        // This is needed even though we're not receiving on the client end...
    }

}