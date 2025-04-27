using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public CameraRotate camScript;
    public EnemyCube enemyControl;
    public KeyboardMove playerControl;
    public GameObject mainMenu;
    public GameObject deathScreen;

    public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetKey(KeyCode.Return))
        {
            camScript.enabled = true;
            mainMenu.SetActive(false);

            playerControl.enabled = true;
            enemyControl.enabled = true;

            gameStarted = true;
        }
    }
}
