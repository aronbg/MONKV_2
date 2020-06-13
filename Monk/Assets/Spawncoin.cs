using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawncoin : MonoBehaviour
{
    
    public AudioSource collectsound;

    private void OnTriggerEnter(Collider other)
    {
        collectsound.Play();
        
        SafeInfo.thehealth += 1;

        PlayerMovement.collected = true;
        PlayerMovement.monkeyarms += 10;

        Destroy(gameObject);
    }
}