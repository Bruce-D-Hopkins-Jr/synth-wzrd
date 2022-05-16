using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    PlayerController playerController;
    SpawnManager spawnManager;

    public GameObject gameOver;
    public GameObject cursor;

    public TMP_Text health;
    public TMP_Text data;
    public TMP_Text rounds;
    public TMP_Text enemies;
    public TMP_Text enemiesDefeated;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "HEALTH: " + playerController.health + " / " + playerController.maxHealth;
        data.text = "DATA: " + playerController.data + " / " + playerController.maxData;

        rounds.text = "ROUND: " + spawnManager.roundCount;
        enemies.text = "ENEMIES: " + spawnManager.enemies;

        enemiesDefeated.text = "ENEMIES DEFEATED: " + spawnManager.totalEnemiesDefeated;

        if (playerController.health <= 0)
        {
            playerController.health = 0;
            playerController.data = 0;
            playerController.speed = 0;

            GameObject[] health = GameObject.FindGameObjectsWithTag("Health");
            GameObject[] data = GameObject.FindGameObjectsWithTag("Data");

            foreach (GameObject element in health)
            {
                Destroy(element);
            }

            foreach (GameObject element in data)
            {
                Destroy(element);
            }

            Cursor.lockState = CursorLockMode.None; 
            cursor.SetActive(false);
            gameOver.SetActive(true);
        }
    }
}