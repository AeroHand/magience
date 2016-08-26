using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Alert : NetworkBehaviour
{


    public GameObject ownUnit;
    public List<GameObject> badguys = new List<GameObject>();

    public int ownteam;
    // Use this for initialization
    void Start () {
        ownteam = ownUnit.GetComponent<TeamTag>().teamnum;
	}

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer) {
            return;
        }
        
        var hit = coll.gameObject;
        var team = hit.GetComponent<TeamTag>();
        if (team != null) {
            if (team.ishero)
            {
                Debug.Log("!!");
            }

            if (team.teamnum != ownteam) {

                badguys.Add(hit);
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (!isServer)
        {
            return;
        }
        var hit = coll.gameObject;
        badguys.Remove(hit);
    }

}
