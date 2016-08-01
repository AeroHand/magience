using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour
{

    public GameObject uipanelPlayerStats;

	// Use this for initialization
	void Awake () {
        //Debug.Log("???");
        uipanelPlayerStats = GameObject.Find("PlayerStats");
        uipanelPlayerStats.SetActive(false);
        if (isServer) {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
