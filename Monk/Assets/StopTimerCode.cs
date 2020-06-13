using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimerCode : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Timercode").SendMessage("recess");
    }
}