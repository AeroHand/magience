using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CreepController : NetworkBehaviour {

    public GameObject creepAvatar;
    Animator animator;

    public GameObject alertchild;
    private Alert scriptAlert;
    public GameObject gameobjBullet;
    public Transform[] pathpoint = new Transform[3];
    public int pathpointer = 1;
    public float firerate = 0.6f;
    private float fireratetimer = 0;
    private int ownteam;
    // Use this for initialization
    void Start () {
        scriptAlert = alertchild.GetComponent<Alert>();
        ownteam = this.GetComponent<TeamTag>().teamnum;
        animator = creepAvatar.GetComponent<Animator>();
    }
    void TheStart(int v)
    {   // you can't use start. But this is just as good.

        this.GetComponent<TeamTag>().teamnum = v;

    }
    // Update is called once per frame
    void Update () {
        //check if he should attack or walk

        if (!isServer)
        {
            return;
        }


        checkstate();
        


        //attack


    }

    
    void checkstate() {
        if (scriptAlert.badguys.Count > 0)
        {
            if (scriptAlert.badguys[0] != null)
            {
                //attack
                var tempShootingV3 = scriptAlert.badguys[0].transform.position - this.transform.position;
                tempShootingV3.Normalize();
                float rot_z = Mathf.Atan2(tempShootingV3.y, tempShootingV3.x) * Mathf.Rad2Deg;
                creepAvatar.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                //check fire rate
                fireratetimer += Time.deltaTime;
                if (fireratetimer >= firerate)
                {
                    fireratetimer = 0;


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
        else
        {
            //walk
            //check if has reached the final point

            if (!isServer)
            {
                return;
            }

            if (Vector3.Distance(this.transform.position, pathpoint[pathpointer].position) <= 1)
            {
                //yes, go to next point
                pathpointer = 2;

            }

            //no, go
            Vector3 tempMovingV3 = pathpoint[pathpointer].position - this.transform.position;
            tempMovingV3.Normalize();
            this.transform.position += tempMovingV3 * Time.deltaTime * 3;

            animator.SetTrigger("walkleft");
        }
    }

    [Command]
    void CmdCreepFire(Vector3 cmdv3shotingdir)
    {
        GameObject tempBullet = Instantiate(gameobjBullet, this.transform.position + cmdv3shotingdir * 1.2f, Quaternion.identity) as GameObject;
        //BulletController tempscript = tempBullet.GetComponent<BulletController>();
        if (cmdv3shotingdir.x > 0)
        {
            doanimationcreep(true, false);
        }
        else {
            doanimationcreep(true, true);
        }
        //tempscript.v3MovingDir = cmdv3shotingdir;
        tempBullet.GetComponent<Rigidbody2D>().velocity = cmdv3shotingdir * 4;
        tempBullet.GetComponent<TeamTag>().teamnum = ownteam;
        NetworkServer.Spawn(tempBullet);
    }


    void doanimationcreep(bool isattack, bool isleft) {
        if (isleft)
        {
            animator.SetTrigger("attackleft");
        }
        else {
            animator.SetTrigger("attackright");
        }
    }
}
