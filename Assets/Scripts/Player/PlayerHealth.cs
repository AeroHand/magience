using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = 100;

    public RectTransform healthBar;
    public GameObject gameobjToken;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
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
        
        
    }


    [Command]
    void CmdSpawnToken()
    {
        GameObject tempBullet = Instantiate(gameobjToken, this.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(tempBullet);
    }
}
