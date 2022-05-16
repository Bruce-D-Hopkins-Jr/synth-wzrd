using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject wyrm;
    private int spawnCount = 10;
    public int roundCount = 1;
    public int enemies;
    public int totalEnemiesDefeated = 0;
    public int enemiesDefeated = 0;

    // Start is called before the first frame update
    void Start()
    {
        RoundOne();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated == spawnCount)
        {
            spawnCount += 5;

            enemiesDefeated = 0;
            
            enemies = spawnCount;

            roundCount++;

            StartCoroutine(SpawnWyrms(spawnCount));
        }
        /*
        else if (enemiesDefeated == 25 && roundCount == 2)
        {
            roundCount++;
            enemies = 20;
            spawnCount += 5;
            StartCoroutine(SpawnWyrms(spawnCount));
        }

        else if (enemiesDefeated == 45 && roundCount == 3)
        {
            roundCount++;
            enemies = 25;
            spawnCount += 5;
            StartCoroutine(SpawnWyrms(spawnCount));
        }

        else if (enemiesDefeated == 70 && roundCount == 4)
        {
            roundCount++;
            enemies = 30;
            spawnCount += 5;
            StartCoroutine(SpawnWyrms(spawnCount));
        }
        */
    }

    IEnumerator SpawnWyrms(int spawn)
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < spawn; i++)
        {
            Vector3 randomLocation1 = new Vector3(UnityEngine.Random.Range(-18, 18), 1.5f, UnityEngine.Random.Range(-18, 18));
            Instantiate(wyrm, randomLocation1, wyrm.transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }
    }

    void RoundOne()
    {
        enemies = 10;
        StartCoroutine(SpawnWyrms(spawnCount));
    }

}