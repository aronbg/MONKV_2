    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimercode : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Timercode").SendMessage("recessisover"); //send a message to start the timer;
        
        
    }

        
}
