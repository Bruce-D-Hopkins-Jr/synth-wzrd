using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody wyrmRB;

    Rigidbody projectileRB;

    Rigidbody playerRB;

    int rangedAttackDamage;
    float rangedAttackSpeed = 75f;


    // Start is called before the first frame update
    void Start()
    {
        wyrmRB = GameObject.FindGameObjectWithTag("Wyrm").GetComponent<Rigidbody>();

        projectileRB = GetComponent<Rigidbody>();

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (wyrmRB != null)
        {
            projectileRB.AddRelativeForce(wyrmRB.transform.forward * rangedAttackSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(transform.position, playerRB.transform.position) > 20.0f)
        {
            Destroy(gameObject);
        }

        else if (wyrmRB == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rangedAttackDamage = UnityEngine.Random.Range(1, 3);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().health -= rangedAttackDamage;

            Debug.Log("Player Health: " + collision.gameObject.GetComponent<PlayerController>().health);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}