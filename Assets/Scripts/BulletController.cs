using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletController : MonoBehaviour
{


    public Vector3 v3MovingDir;
    public int ownteam;

    public float floLivingTime=4;
	// Use this for initialization
	void Start () {
        ownteam = this.GetComponent<TeamTag>().teamnum;
	}
	
	// Update is called once per frame
	void Update () {
    }

    //hit
    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;
        var health = hit.GetComponent<PlayerHealth>();
        if (health != null)
        {
            var teamtag = hit.GetComponent<TeamTag>().teamnum;
            Debug.Log(teamtag.ToString() + " bulletteam: " + ownteam.ToString());
            if (teamtag != ownteam)
            {
                health.TakeDamage(10);
            }

        }
        else {
            var healthunique = hit.GetComponent<PlayerHealthUnique>();
            if (healthunique != null)
            {
                var teamtag = hit.GetComponent<TeamTag>().teamnum;
                Debug.Log(teamtag.ToString() + " bulletteam: " + ownteam.ToString());
                if (teamtag != ownteam)
                {
                    healthunique.TakeDamage(10);
                }

            }
        }

        Destroy(gameObject);
    }
}
