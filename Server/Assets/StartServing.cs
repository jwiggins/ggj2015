using UnityEngine;
using System.Collections;

public class StartServing : MonoBehaviour {

    public int connectionPort = 25001;
    public string connectedAddr;

    void OnPlayerConnected(NetworkPlayer player)
    {
        connectedAddr = player.ipAddress;
    }

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
            if (GUI.Button(new Rect(10, 50, 120, 20), "Start Server"))
            {
                Network.InitializeServer(32, connectionPort, false);
            }
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            string ipAddr = Network.player.ipAddress;
            GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Server");
            GUI.Label(new Rect(10, 30, 300, 20), ipAddr);
            GUI.Label(new Rect(10, 50, 300, 20), "client: " + connectedAddr);
            if (GUI.Button(new Rect(10, 70, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
    }
}