using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotFollowpath : MonoBehaviour
{
    public List<Transform> PathSequence = new List<Transform>(); //Array of all points in the path
    public Transform Parrot;
    private bool playerisneartheparrot;
    int currentlocation;
    public LayerMask playerlayer;
    public float speed = 5f;
    public Animator flyanimation;
    public float howclosetothebird = 30f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
        
        playerisneartheparrot = Physics.CheckSphere(PathSequence[currentlocation].position, howclosetothebird, playerlayer) ;
if (playerisneartheparrot)
        {
            currentlocation += 1;

            playerisneartheparrot = false;
        }
        if (currentlocation>= 17 && currentlocation<23)
        {
            howclosetothebird = 50f;

        }
        if (currentlocation >= 23 && currentlocation<26)
        {
            howclosetothebird = 30f;

        }
        if (currentlocation >= 30 && currentlocation < 40)
        {
            howclosetothebird = 60f;

        }
        if (currentlocation >= 43)
        {
            speed = 0.44f;
            howclosetothebird = 100f;

        }
        if (Parrot.transform.position == PathSequence[currentlocation].position){
            flyanimation.SetBool("Fly", false);

        }
        else
        {
            flyanimation.SetBool("Fly", true);
        }

        Parrot.transform.position = Vector3.Lerp(Parrot.transform.position,
                                       PathSequence[currentlocation].transform.position,
                                       Time.deltaTime * speed);
        
    }

}
