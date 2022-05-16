using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    SpawnManager spawnManager;

    Rigidbody playerRB;

    Rigidbody projectileRB;
    int playerProjectileDamage;

    float speed = 200;

    public GameObject[] items;    

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        projectileRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileRB.AddForce(projectileRB.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);

        if (Vector3.Distance(transform.position, playerRB.transform.position) > 20.0f)
        {
            Destroy(gameObject);
        }
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        playerProjectileDamage = UnityEngine.Random.Range(1, 5);

        if (collision.gameObject.CompareTag("Wyrm"))
        {
            collision.gameObject.GetComponent<StandardWyrm>().health -= playerProjectileDamage;

            if (collision.gameObject.GetComponent<StandardWyrm>().health <= 0)
            {
                int itemDrop = Random.Range(0, 6);
                Instantiate(items[itemDrop], collision.gameObject.transform.position + new Vector3(0, 1, 0), collision.gameObject.transform.rotation);                
                Destroy(collision.gameObject);
                spawnManager.totalEnemiesDefeated++;
                spawnManager.enemiesDefeated++;
                spawnManager.enemies--;                
            }
            
            Debug.Log("Wyrm Health: " + collision.gameObject.GetComponent<StandardWyrm>().health);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
    
}
