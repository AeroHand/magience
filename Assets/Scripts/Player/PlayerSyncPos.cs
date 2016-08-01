using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPos : NetworkBehaviour
{
    [SyncVar]
    public Vector3 sycpos;

    [SerializeField]
    Transform myTransform;

    [SerializeField]
    float lerprate = 15;

    void FixedUpdate() {
        TransmitPos();
        LerpPostion();
    }

    void LerpPostion() {
        if (!isLocalPlayer) {
            myTransform.position = Vector3.Lerp(myTransform.position, sycpos, Time.deltaTime * lerprate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {

        sycpos = pos;
    }

    [ClientCallback]
    void TransmitPos()
    {
        //Debug.Log("sending");
        if (isLocalPlayer)
        {
            
            CmdProvidePositionToServer(myTransform.position);
        }
    }
}
