using UnityEngine;
using System.Collections;

public class NetworkSetup : MonoBehaviour {
 
    public string connectionIP;
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
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client");
            if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
    }
}