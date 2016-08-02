﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealthUnique : NetworkBehaviour
{

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = 100;


    public int fullHealth = 100;

    public RectTransform healthBar;
    public GameObject gameobjToken;

    public RectTransform UIhealthBar;
    public Text UIHPText;


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        UIhealthBar=GameObject.Find("GreenHP").GetComponent<RectTransform>();
        UIHPText = GameObject.Find("HPText").GetComponent<Text>();
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

            Destroy(gameObject);

        }

    }

    void OnChangeHealth(int health)
    {

        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        UIhealthBar.offsetMax=new Vector2(- 250 * (100 - health)/100f, UIhealthBar.offsetMax.y);
        UIHPText.text = (health).ToString()+"%";
    }


    [Command]
    void CmdSpawnToken()
    {
        GameObject tempBullet = Instantiate(gameobjToken, this.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(tempBullet);
    }
}

