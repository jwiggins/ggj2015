using UnityEngine;
using System.Collections;

public class StartServing : MonoBehaviour {

    public int connectionPort = 25001;

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
            if (GUI.Button(new Rect(10, 50, 120, 20), "Disconnect"))
            {
                Network.Disconnect(200);
            }
        }
    }
}