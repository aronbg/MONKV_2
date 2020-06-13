using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreScript : MonoBehaviour
{
  
    public AudioSource collectsound;

    private void OnTriggerEnter(Collider other)
    {
        collectsound.Play();
        SafeInfo.thescore += 1;
        PlayerMovement.monkeyarms += 10; 
       
        Destroy(gameObject);
    }
}
