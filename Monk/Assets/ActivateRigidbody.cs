using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRigidbody : MonoBehaviour
{
    public LayerMask playerlayer;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.CheckSphere(transform.position, 220f, playerlayer)){
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }


    }
}
