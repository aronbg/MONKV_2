using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timercode : MonoBehaviour
{
    public Text TimerText;
    private float startTime = 0f;
    private bool stop = true;
    private bool finnished = false;
    public AudioSource monkeysound;
    public AudioSource Win;
    private float recesstime;
    float t;

    void startfrombegin ()
    {
        
        startTime = Time.time;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (stop == true)
        {
            return;
        }
        else
        {
            
     
             t = Time.time - startTime ;
            
           string minutes = ((int)t / 60).ToString();
           string seconds = (t % 60).ToString("f0");
            TimerText.text = minutes + ":" + seconds;
        }
    
    }
  
   
    void recessisover()
    {
        
        stop = false;
       
        TimerText.fontSize =80;
        
        TimerText.fontStyle = FontStyle.Bold;
    }
    void finished()
    {
        stop = true;
        Win.Play();
        Invoke("cooldownafterwin", 5f);
        TimerText.fontSize = 130;
        TimerText.color = Color.green;
    }
    void cooldownafterwin()
    {
        monkeysound.Play();
        Invoke("gotomainmenu", 10f);

    }
    void gotomainmenu()
    {
        SafeInfo.thehealth = 0;
        SafeInfo.thescore = 0;
        startTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    }


