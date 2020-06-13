using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthScript : MonoBehaviour
{

    public AudioSource collectsound;

    private void OnTriggerEnter(Collider other)
    {
        collectsound.Play();
      
        SafeInfo.thehealth += 1;
       

        Destroy(gameObject);
    }
}

