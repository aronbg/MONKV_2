using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeInfo : MonoBehaviour
{
    // Start is called before the first frame update

 
    public GameObject scoretext;
    public GameObject leveltext;
    public static int thescore;
    public static int thehealth;
    bool stopthis;




   
   private void Update()
    {


       
        
        leveltext.GetComponent<Text>().text = "Health:" + thehealth;
        if (stopthis == true)
        {
            scoretext.GetComponent<Text>().text = "Ultimate Arms";
            scoretext.GetComponent<Text>().fontSize = 20;


        }
        else
        {
            scoretext.GetComponent<Text>().text = "ArmLength:" + thescore;
        }
    }
    void ultimate()
    {
        stopthis = true;
    }
}
