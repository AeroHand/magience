﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealthUnique : NetworkBehaviour
{

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = 100;

    [SyncVar(hook = "OnChangeToken")]
    public int currentToken = 0;


    public int fullHealth = 100;

    
    public GameObject gameobjToken;

    public GameObject gbar;
    public GameObject hbar;

    public RectTransform UIhealthBar;
    public Text UIHPText;

    public Text UITokennum;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        UIhealthBar=GameObject.Find("GreenHP").GetComponent<RectTransform>();
        UIHPText = GameObject.Find("HPText").GetComponent<Text>();
        UITokennum = GameObject.Find("TokenText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");

            CmdSpawnToken();

            if (this.GetComponent<TeamTag>().teamnum == 1)
            {
                this.transform.position = new Vector3(60, 87, 0);
                currentHealth = 100;
            }
            else {
                this.transform.position = new Vector3(116, 143, 0);
                currentHealth = 100;
            }


        }

    }

    void OnChangeHealth(int health)
    {
        gbar.transform.localScale = new Vector3((health *1f/ fullHealth), gbar.transform.localScale.y, gbar.transform.localScale.z);
        gbar.transform.position = hbar.transform.position + Vector3.left * (fullHealth - health) / (fullHealth)*2;

        UIhealthBar.offsetMax=new Vector2(- 250 * (100 - health)/100f, UIhealthBar.offsetMax.y);
        UIHPText.text = (health).ToString()+"%";
    }

    void OnChangeToken(int tokennum)
    {

        UITokennum.text = (tokennum).ToString();
    }


    [Command]
    void CmdSpawnToken()
    {
        GameObject tempBullet = Instantiate(gameobjToken, this.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(tempBullet);
    }
}

