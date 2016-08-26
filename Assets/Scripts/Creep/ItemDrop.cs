using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemDrop : NetworkBehaviour {

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
                hit.GetComponent<PlayerHealthUnique>().currentToken += 1;
                Destroy(gameObject);

            }
        }
    }
}
