using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerSpawn;
    public GameObject enemySpawn;

    public KeyboardMove player;
    public EnemyCube enemy;
    public CameraRotate cam;

    public float deathHeight = -5.0f;

    public bool gameActive = false;
    public int deaths = 0;
    public int wins = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

        if (player.transform.position.y < deathHeight)
        {
            PlayerRespawn();
            deaths++;
        }

        if (enemy.transform.position.y < deathHeight)
        {
            EnemyRespawn();
            enemy.ChangeMetal();
            wins++;
        }
    }

    // enables/disables the gameplay and controls for cam, enemy and player
    public void GameActive(bool value)
    {
        player.GetComponent<KeyboardMove>().isActive = value;
        enemy.GetComponent<EnemyCube>().isActive = value;   
        cam.isActive = value;
        gameActive = value;
    }

    public void EnemyRespawn()
    {
        enemy.transform.position = enemySpawn.transform.position;
        enemy.transform.rotation = enemySpawn.transform.rotation;
    }

    public void PlayerRespawn()
    {
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
    }
}
