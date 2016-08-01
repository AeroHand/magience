using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {

    public void StartupHost() {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame() {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }


    void SetIPAddress() {
        string ipAddress = GameObject.Find("InputFiledIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;

    }
}
