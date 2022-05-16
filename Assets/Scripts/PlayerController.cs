using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float time;    

    //Player Rigidbody
    Rigidbody playerRB;

    //Camera GameObject
    Camera cam;

    //Projectile GameObject
    public GameObject[] projectile;
    
    //Player Movement Variables
    public float speed = 6.0f;
    float turnSpeed = 35.0f;
    float lookSpeed = 65.0f;
    float jump = 20.0f;
    float xRotation;

    //Player Health and Data Variables
    public int health = 25;
    public int maxHealth = 25;
    public int data = 1000000;
    public int maxData = 1000000;

    //Player bool values
    bool isJumping;

    //Audio Sources
    public AudioSource fireSound;
    public AudioSource jumpSound;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;        

        //Turn Player using mouse
        Turn();

        //Look up and down using mouse
        Look();

        //Move Player
        Move();

        //Jump using spacebar
        Jump();

        //Press left mouse button to shoot spells
        StartCoroutine(Fire());

        if (data >= maxData)
        {
            data = maxData;
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }

    }   

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * moveX * Time.deltaTime * speed);

        float moveZ = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * moveZ * Time.deltaTime * speed);
    }

    private void Turn()
    {
        //Use mouse to turn player left and right
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * mouseX);
    }

    private void Look()
    {
        //Use mouse to rotate camera up and down
        float mouseY = Input.GetAxis("Mouse Y");
        cam.transform.Rotate(Vector3.left, lookSpeed * Time.deltaTime * mouseY);

        //Restrict up and down camera movement
        xRotation = cam.transform.localEulerAngles.x;
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    //Jump function
    private void Jump()
    {
        //Use spacebar to jump (Also prevents unlimited jumps while in air)
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpSound.Play();
            playerRB.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            isJumping = true;
        }        
    }

    IEnumerator Fire()
    {
        if (data >= 2)
        {
            if (Input.GetMouseButtonDown(0) && time >= .2f)
            {                
                for (int i = 0; i < 2; i++)
                {
                    fireSound.Play();
                    Instantiate(projectile[0], GameObject.Find("Fire Position").transform.position, cam.transform.rotation);
                    yield return new WaitForSecondsRealtime(.1f);
                }
                time = 0;
                data -= 2;
            }            
        }
    }


    //When in contact with another collider set both jumps to false
    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;        
    }
}