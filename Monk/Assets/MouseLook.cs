using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Camera playercamera;
    private float targetFov;
    private float fov;


    // private Camera playercamera;
    // Start is called before the first frame update

    private void Awake()
    {
        playercamera = GetComponent<Camera>();
        targetFov = playercamera.fieldOfView;
        fov = targetFov;
    }

    // Update is called once per frame

   private void Update()
    {
        float fovspeed = 4f;
        fov = Mathf.Lerp(fov, targetFov, Time.deltaTime * fovspeed);
        playercamera.fieldOfView = fov;


      
    }
    public void SetCameraFov(float targetFov)
    {
        this.targetFov = targetFov;
    }
}