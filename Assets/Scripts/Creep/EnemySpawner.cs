using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public int teamspawn;

    public Transform[] pathpoint = new Transform[3];

    public override void OnStartServer()
    {
        
    }

    private float refreshtime = 0;

    void Update() {
        refreshtime += Time.deltaTime;
        //spawn a wave of creeps
        if (refreshtime >= 15) {
            refreshtime = 0;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var spawnPosition = this.transform.position + new Vector3(
                    Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);


                var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemy.SendMessage("TheStart",teamspawn);
                enemy.GetComponent<CreepController>().pathpoint = pathpoint;
                //enemy.GetComponent<TeamTag>().teamnum = teamspawn;
                NetworkServer.Spawn(enemy);
            }

        }
    }
}
