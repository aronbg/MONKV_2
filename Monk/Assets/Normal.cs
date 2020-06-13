using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement.normal = true;

    }
}   