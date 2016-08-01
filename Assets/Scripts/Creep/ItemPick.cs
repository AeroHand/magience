using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemPick : NetworkBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer)
        {
            return;
        }

        var hit = coll.gameObject;
        var team = hit.GetComponent<TeamTag>();
        if (team != null)
        {
            if (team.ishero)
            {
                Destroy(gameObject);

            }
        }
    }
}
