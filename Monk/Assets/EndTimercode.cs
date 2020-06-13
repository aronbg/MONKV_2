using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimercode : MonoBehaviour
{
    public GameObject endgame;
    private void OnTriggerEnter(Collider other)
    {//end the timer code
        GameObject.Find("Timercode").SendMessage("finished");
        PlayerMovement.monkeyarms = 15f;
        PlayerMovement.momentumextraspeed = 2f;
        PlayerMovement.hookshotthrowspeed = 500f;
        PlayerMovement.hookshotspeedmultiplier = 2f;
        PlayerMovement.hookshotspeedmax = 100f;
        PlayerMovement.hookshotspeedmin = 10f;
        PlayerMovement.walkspeed = 20f;
        PlayerMovement.runspeed = 26f;
    }
}
