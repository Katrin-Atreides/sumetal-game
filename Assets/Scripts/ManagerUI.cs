using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject deathScreen;
    public GameObject gameTime;
    public GameManager gameManager;

    public TMP_Text deathCounter;
    public TMP_Text winCounter;
    public TMP_Text enemyType;
    
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameActive && Input.GetKey(KeyCode.Return))
        {
            mainMenu.SetActive(false);
            gameTime.SetActive(true);
            gameManager.GameActive(true);
            source.Play();
        }

        if (gameManager.gameActive)
        {
            deathCounter.text = "Deaths: "+gameManager.deaths.ToString();
            winCounter.text = "Wins: "+gameManager.wins.ToString();
            enemyType.text = "you VS "+ gameManager.enemy.currentMetalName;
        }
    }
}
