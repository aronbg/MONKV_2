using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperArms : MonoBehaviour
{
    public AudioSource banana;
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement.momentumextraspeed = 9f;
        PlayerMovement.monkeyarms = 800f;
        PlayerMovement.hookshotthrowspeed = 1000f;
        PlayerMovement.hookshotspeedmultiplier = 2.5f;
        PlayerMovement.hookshotspeedmin = 50f;
        PlayerMovement.hookshotspeedmax = 200f;
        PlayerMovement.walkspeed = 50f;
        PlayerMovement.runspeed = 50f;
        SafeInfo.thescore =1000;
        GameObject.Find("ScoreText").SendMessage("ultimate");
        banana.Play();
        Destroy(gameObject);
    }
}
