using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starttimerfrombegin : MonoBehaviour
{
    public AudioSource monksound;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        monksound.Play();
        GameObject.Find("Timercode").SendMessage("startfrombegin");
        GameObject.Find("Timercode").SendMessage("recessisover");
        Destroy(gameObject);
    }
}
