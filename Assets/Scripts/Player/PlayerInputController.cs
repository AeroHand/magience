using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerInputController : NetworkBehaviour {
    public GameObject maincamera;

    public GameManager scriptGameManager;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        maincamera.SetActive(true);
        ownteam = this.GetComponent<TeamTag>().teamnum;
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptGameManager.uipanelPlayerStats.SetActive(true);
    }

    

    public GameObject gameobjBullet;
    public GameObject gameobjHeroAva;


    public float floFireRate;

    private float inputX;
    private float inputY;

    private Vector3 tempShootingV3;
    private int ownteam;
    public float floShootRate=0.4f;
    private float flosShootTimer=0;
    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer) {
            return;
        }

        //movement
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Vector3 tempMovingV3 = new Vector3(inputX, inputY, 0);
        tempMovingV3.Normalize();
        this.transform.position += tempMovingV3*Time.deltaTime*10;

        //shooting
        inputX = Input.GetAxis("HorizontalFire");
        inputY = Input.GetAxis("VerticalFire");
        tempShootingV3 = new Vector3(inputX, inputY, 0);
        tempShootingV3.Normalize();

        if (tempShootingV3.magnitude > 0.4f)
        {
            //player is shooting

            //change facing vec3

            float rot_z = Mathf.Atan2(tempShootingV3.y, tempShootingV3.x) * Mathf.Rad2Deg;
            gameobjHeroAva.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


            //check fire rate
            if (flosShootTimer <= 0)
            {
                CmdFire(tempShootingV3);
                flosShootTimer = floShootRate;
            }
            else {
                flosShootTimer -= Time.deltaTime;
            }
            
        }
        else
        {
            if (tempMovingV3.magnitude > 0.4f)
            {
                float rot_z = Mathf.Atan2(tempMovingV3.y, tempMovingV3.x) * Mathf.Rad2Deg;
                gameobjHeroAva.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            }
        }

    }


    public void SetTeam(int curteam) {

        if (!isLocalPlayer)
        {
            return;
        }

        ownteam = curteam;
        this.GetComponent<TeamTag>().teamnum = curteam;
        if (curteam == 1)
        {
            this.transform.position = new Vector3(60,87,0);
        }
        else
        {
            this.transform.position = new Vector3(88, 115, 0);
        }
    }

    [Command]
    void CmdFire(Vector3 cmdv3shotingdir)
    {
        GameObject tempBullet = Instantiate(gameobjBullet, this.transform.position+ cmdv3shotingdir*1.4f, Quaternion.identity) as GameObject;
        //BulletController tempscript = tempBullet.GetComponent<BulletController>();
        //tempscript.v3MovingDir = cmdv3shotingdir;
        tempBullet.GetComponent<Rigidbody2D>().velocity = cmdv3shotingdir*12;
        tempBullet.GetComponent<TeamTag>().teamnum = ownteam;
        NetworkServer.Spawn(tempBullet);
    }

}
