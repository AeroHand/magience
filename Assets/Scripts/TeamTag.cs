using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TeamTag : NetworkBehaviour
{
    [SyncVar]
    public int teamnum;
    public bool ishero;
}
