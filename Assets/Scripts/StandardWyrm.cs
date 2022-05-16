using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class StandardWyrm : MonoBehaviour
{
    PlayerController playerController;

    Rigidbody player;
    Rigidbody wyrmRB;

    public GameObject projectile;
    public GameObject firePosition;
    public AudioSource wyrmHiss;

    float time;

    float followDistance = 3.0f;

    public int health;
    float speed = 4.0f;

    bool isOnGround;    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        player = GameObject.Find("Player").GetComponent<Rigidbody>();

        wyrmRB = GetComponent<Rigidbody>();

        health = Random.Range(10, 16);       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        FollowPlayer();

        MeleeAttack();

        // RangedAttack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            int meleeAttack = Random.Range(1, 3);

            collision.gameObject.GetComponent<PlayerController>().health -= meleeAttack;
            Debug.Log("Player health: " + collision.gameObject.GetComponent<PlayerController>().health);
        }
    }

    void FollowPlayer()
    {
        transform.LookAt(player.transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void MeleeAttack()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < followDistance && isOnGround)
        {            
            isOnGround = false;
            wyrmHiss.Play();
            wyrmRB.AddRelativeForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            wyrmRB.AddRelativeForce(new Vector3(0, 0, 15), ForceMode.Impulse);           
        }

    }

    /*
    void RangedAttack()
    {
        if (wyrmRB != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > followDistance && time > 8f)
            {
                Instantiate(projectile, firePosition.transform.position, projectile.transform.rotation);
                time = 0;
            }
        }
    }
    */
}