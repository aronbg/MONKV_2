using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathanimation : MonoBehaviour
{
    public Animator animator;
    public AudioSource deathsound;
    private void OnTriggerEnter(Collider other)
    {
        deathsound.Play();
        animator.SetTrigger("fadeout");
    }
}
