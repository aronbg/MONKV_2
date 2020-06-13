using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ubernormalstate : MonoBehaviour
{
   
    


    private void OnTriggerEnter(Collider other)
    {
        
        PlayerMovement.supernormal = true;

    }
}