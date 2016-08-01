using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerController : NetworkBehaviour
{

    public GameObject alertchild;
    private Alert scriptAlert;
    public GameObject gameobjBullet;
    public Transform aim;

    public float firerate = 0.6f;
    private float fireratetimer = 0;

    private int ownteam;
    // Use this for initialization
    void Start () {
        scriptAlert = alertchild.GetComponent<Alert>();
        ownteam = this.GetComponent<TeamTag>().teamnum;
    }
	
	// Update is called once per frame
	void Update () {
        if (scriptAlert.badguys.Count > 0)
        {
            //attack
            if (scriptAlert.badguys[0] != null)
            {
                //check fire rate
                fireratetimer += Time.deltaTime;
                if (fireratetimer >= firerate)
                {
                    fireratetimer = 0;
                    if (scriptAlert.badguys[0] != null)
                    {

                    }
                    else
                    {
                        Debug.Log("alert attack warnnning! list 0 is empty");
                        for (var i = scriptAlert.badguys.Count - 1; i > -1; i--)
                        {
                            if (scriptAlert.badguys[i] == null)
                                scriptAlert.badguys.RemoveAt(i);
                        }
                    }
                    var tempShootingV3 = scriptAlert.badguys[0].transform.position - this.transform.position;
                    tempShootingV3.Normalize();
                    CmdCreepFire(tempShootingV3);
                }
            }
            else
            {
                Debug.Log("alert attack warnnning! list 0 is empty");
                for (var i = scriptAlert.badguys.Count - 1; i > -1; i--)
                {
                    if (scriptAlert.badguys[i] == null)
                        scriptAlert.badguys.RemoveAt(i);
                }
            }

            
        }
    }

    [Command]
    void CmdCreepFire(Vector3 cmdv3shotingdir)
    {
        GameObject tempBullet = Instantiate(gameobjBullet, aim.position + cmdv3shotingdir * 1.4f, Quaternion.identity) as GameObject;
        //BulletController tempscript = tempBullet.GetComponent<BulletController>();
        //tempscript.v3MovingDir = cmdv3shotingdir;
        tempBullet.GetComponent<Rigidbody2D>().velocity = cmdv3shotingdir * 12;
        tempBullet.GetComponent<TeamTag>().teamnum = ownteam;
        NetworkServer.Spawn(tempBullet);
    }
}
