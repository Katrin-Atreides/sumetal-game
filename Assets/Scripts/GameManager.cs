using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerSpawn;
    public GameObject enemySpawn;

    public GameObject player;
    public GameObject enemy;

    public float deathHeight = -5.0f;

    public void Restart()
    {
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;

        enemy.transform.position = enemySpawn.transform.position;
        enemy.transform.rotation = enemySpawn.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < deathHeight)
        {
            player.transform.position = playerSpawn.transform.position;
            player.transform.rotation = playerSpawn.transform.rotation;
        }

        if (enemy.transform.position.y < deathHeight)
        {
            enemy.transform.position = enemySpawn.transform.position;
            enemy.transform.rotation = enemySpawn.transform.rotation;
        }
    }
}
