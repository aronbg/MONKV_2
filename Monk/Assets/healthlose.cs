using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthlose : MonoBehaviour
{

   

    private void OnTriggerEnter(Collider other)
    {
        
        SafeInfo.thehealth -= 1;
        PlayerMovement.respawn = true;

      




    }
}
