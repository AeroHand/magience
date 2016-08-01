using UnityEngine;
using System.Collections;

public class TeamSelection : MonoBehaviour {

    public int teamnum;

    void OnTriggerEnter2D(Collider2D coll)
    {

        var hit = coll.gameObject;
        if (hit.GetComponent<PlayerInputController>())
        {
            hit.GetComponent<PlayerInputController>().SetTeam(teamnum);
        }
    }
}
