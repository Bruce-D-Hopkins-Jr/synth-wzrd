using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVial : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().data < other.gameObject.GetComponent<PlayerController>().maxData)
        {
            other.gameObject.GetComponent<PlayerController>().data += 10;

            Destroy(gameObject);
        }
    }
}
