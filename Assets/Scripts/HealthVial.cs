using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthVial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().health < other.gameObject.GetComponent<PlayerController>().maxHealth)
        {
            other.gameObject.GetComponent<PlayerController>().health += 10;

            Destroy(gameObject);
        }
    }
}
