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
                if (hit.GetComponent<PlayerHealthUnique>().currentToken >= 20)
                {
                    hit.GetComponent<PlayerHealthUnique>().currentToken -= 20;
                    hit.GetComponent<PlayerInputController>().floShootRate -= 0.1f;
                    Destroy(gameObject);
                }
            }
        }
    }
}
