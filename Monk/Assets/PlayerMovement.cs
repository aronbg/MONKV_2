using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Transform Hookshottransform;
    [SerializeField] private Transform Hookshottransform1;
    [SerializeField] private Transform Hookshottransform2;
    [SerializeField] private Transform player;
    public LayerMask floormask;
    public float cooldownforhookshot = 1f;
    float distancehookshot; //distancehookshot
    public static float monkeyarms = 15f; // distance a player is able to hookshot
    public float raycastdistance;
    public CharacterController controller;
    public float MouseSens = 80f; //mousesensitivity
    float xRot = 0f; //rotation for x
    private bool cooldown; //cooldown for bunny hop
    public Transform localRotation;
    public static float momentumextraspeed = 2f;
    public static float hookshotthrowspeed = 500f;
    public static float hookshotspeedmultiplier = 2f;
    public static float hookshotspeedmax = 100f;
    public static float hookshotspeedmin = 10f;
    public static float walkspeed = 22f; // walkingspeed
    public static float runspeed = 28f; // sprintspeed
    public float speed = 14f; //changes to runspeed or walkspeed
    public float gravity = -16f; //gravity
    bool grounded; //is the player grounded
    float hookshotsize; //size of arms
    public Transform groundcheck; // raycast from here to make grounded
    public float grounddistance = 0.15f; // size for grounded
    bool right;// use right arm
    bool left; // use left arm
    private bool cooldownspamclick;//cooldown arm
    private bool cooldownspamclick2; // cooldown second arm
    public static float[] position; // position set of spawnpointbanana
    public static bool collected = false; //moment to set the position of the spawnpointbanana
    public static bool respawn; //respawn player to last spawnpointbanana
    public static bool supernormal; //set state to simple
    public static bool normal; //set state to normal
    Vector3 currentposition; // respawnposition
    public float jumpheight = 20f;
    Vector3 velocity;
    Vector3 charactervelocitymomentum; // extra velocity added when you cancel the handlehookshotmovement
    float velocityy;
    private ParticleSystem speedleavesparticles; //particles while handlehookshotmovement 
    public LayerMask groundmask; // floorlayer
    private Camera playercamera; // view
    private MouseLook cameraFOV;
    private const float normalFOV = 55f; //camerafov during normal
    private const float hookshotFOV = 110f; // camerafov during hookshot
    private const float sprintFOV = 70f; // camerafov during sprint
    private State state; //states
    private Vector3 hookshotposition;//position of hookshot
    public Image knob;
    private LayerMask Playermask;
    public Animator animator;

    public Text instruction;




    private void Awake()
    {
        speedleavesparticles = transform.Find("MainCamera").Find("ParticleSystem").GetComponent<ParticleSystem>();
        playercamera = transform.Find("MainCamera").GetComponent<Camera>();
        cameraFOV = playercamera.GetComponent<MouseLook>();
        groundcheck = transform.Find("groundcheck");
        speedleavesparticles.Stop();

        Playermask = LayerMask.GetMask("Floor");



    }

    private enum State //place where i create my states
    {
        Normal,
        HookshotFlyinPlayer,
        Hookshotthrown,
        simple
    }
    void Start()
    {

        transform.position = new Vector3(883f, 77f, 16f); //startposition at all times

        Cursor.lockState = CursorLockMode.Locked; //remove cursor from screen
        Cursor.visible = false;
        instructionexplainfirstone();


    }



    // Update is called once per frame
    void Update()
    {

        
        spawnmaker();
        Grounded();
        if (testhookshotinputleft()) //choose my hookshot (left or right)
        {
            left = false;
            right = true;
            stophookshot(); //stop the hookshot so it doesnt stop during the hookshot
        }
        if (testhookshotinputright())
        {
            left = true;
            right = false;
            stophookshot();
        }
        changestatetosimple(); //if you hit an ubernormalstate position, change to simple.
        switch (state)
        {
            default:
            case State.Normal:

                hookshotstart();

                movement();
                mouselooking();
                Calculatedistance();
                //you can look move and start a hookshot


                break;

            case State.HookshotFlyinPlayer:

                handlehookshotmovement();
                mouselooking();
                //you can look and your movement is the movement for the flying player


                break;


            case State.Hookshotthrown:

                handlehookshotthrow();

                mouselooking();
                movement();

                // you increase the object till it hits the raycastpoint you can still move and look

                break;
            case State.simple:
                movement();
                mouselooking();
                changestatetonormal();
                //you are in a no hookshot spot
                break;
        }



    }



    void spawnmaker() //creates a spawn point when spawncoin is collected
    {

        if (collected)
        {

            position = new float[3];
            position[0] = transform.position.x;
            position[1] = transform.position.y;
            position[2] = transform.position.z;
            currentposition = new Vector3(position[0], position[1], position[2]);
            collected = false;

        }


    }
    public void instructionexplainfirstone()
    {
        instruction.text = "Oh shit, that bird just stole your hat!";
        Invoke("instructionexplainsecondone", 5f);
    }
    public void instructionexplainsecondone()
    {
        instruction.text = "You'll get him for that! ";
        Invoke("instructionexplain", 4f);
    }
    public void instructionexplain()
    {
        instruction.text = "Press Shift to add extra speed ";
        Invoke("instructionexplainjump", 6f);

    }
    public void instructionexplainjump()
    {
        instruction.text = "Press Space to jump";
        Invoke("instructionexplain2", 4f);
    }
    public void instructionexplain2()
    {
        instruction.text = "Press the right or left mouse button to climb";
        Invoke("instructionexplainextra1", 8f);
    }
    public void instructionexplainextra1()
    {
        instruction.text = "You can jump while being in the air to create Extra momentum.";
        Invoke("instructionexplain3", 10f);
    }
    public void instructionexplain3()
    {
        instruction.text = "Collect Banana's to make your arms longer and hearths for health";
        Invoke("instructionexplain4", 6f);
    }
    public void instructionexplain4()
    {
        instruction.text = "Have Fun!";
        Invoke("skipinstructions", 3f);
    }
    public void skipinstructions()
    {
        instruction.text = "";
    }
    void movement() //you can move

    {

        if (testsprintinput())
        {

            speed = runspeed;

            cameraFOV.SetCameraFov(sprintFOV);

        }
        if (testsprintoutput())
        {
            speed = walkspeed;
            cameraFOV.SetCameraFov(normalFOV); //camera is set to another fov when sprinting


        }



        float x = Input.GetAxis("Horizontal"); //find the wasd buttons
        float z = Input.GetAxis("Vertical");


        Vector3 velocity = transform.right * x + transform.forward * z; //create an movement on velocity vector
        velocity = velocity * speed;
        if (grounded && velocityy < 0f)
        {

            resetgravity();
            // velocity y has been changed

        }
        if (testjumpinput() && grounded == false)
        {
            Invoke("resetcooldown", 0.2f);
            cooldown = true;
        }
        if ((testjumpinput() && grounded) || (cooldown && grounded))
        {

            velocityy = Mathf.Sqrt(jumpheight * -2f * gravity);

        }
        velocityy += gravity * Time.deltaTime;
        velocity.y = velocityy;


        if (charactervelocitymomentum.magnitude > 0f)
        {

            float dragmomentum = 4f;
            charactervelocitymomentum -= charactervelocitymomentum * dragmomentum * Time.deltaTime;
            if (charactervelocitymomentum.magnitude <= .0f)
            {
                Debug.Log("kom");

                charactervelocitymomentum = Vector3.zero;


            }

        }

        velocity += charactervelocitymomentum;
        controller.Move(velocity * Time.deltaTime);
        if (respawn == true)
        {
            if (SafeInfo.thehealth >= 0)
            {

                transform.position = currentposition;
                Invoke("resetrespawn", 1f);

            }
            else if (SafeInfo.thehealth < 0)
            {
                SafeInfo.thehealth = 0;
                SafeInfo.thescore = 0;
                monkeyarms = 15f;
                momentumextraspeed = 2f;
                hookshotthrowspeed = 500f;
                hookshotspeedmultiplier = 2f;
                hookshotspeedmax = 100f;
                hookshotspeedmin = 10f;
                walkspeed = 20f;
                runspeed = 26f;
                Invoke("gobacktomenu", 1f); //wait a second so the music can play and it doesnt feel to sudden


            }
        }


    }
    private void gobacktomenu()
    {
        //ik heb besloten dit er niet in te doen.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void resetgravity()
    {


        velocityy = 0f;
    }
    void Calculatedistance()
    {
        if (Physics.Raycast(playercamera.transform.position, playercamera.transform.forward, out RaycastHit rayhittie, monkeyarms, floormask))
        {
            knob.color = Color.green;
        }
        else
            knob.color = Color.white;
    }

    private void hookshotstart()
    {




        if (testhookshotinputright() && cooldownspamclick2 == false)
        { //if right is clicked make an cooldown for it

            if (Physics.Raycast(playercamera.transform.position, playercamera.transform.forward, out RaycastHit raycastHit, monkeyarms, floormask))// then start the raycast.
            {

                hookshotposition = raycastHit.point;
                hookshotsize = 0f;
                state = State.Hookshotthrown;
                resetgravity();

            }
        }

        if (testhookshotinputleft() && cooldownspamclick == false)
        {




            if (Physics.Raycast(playercamera.transform.position, playercamera.transform.forward, out RaycastHit raycastHit, monkeyarms, floormask))
            {

                hookshotposition = raycastHit.point;
                hookshotsize = 0f;
                state = State.Hookshotthrown;
                resetgravity();

            }
        }
    }



    private void handlehookshotthrow()
    {

        if (left == true)
        {

            Hookshottransform = Hookshottransform1;
        }
        if (right == true)
        {

            Hookshottransform = Hookshottransform2;
        }

        Hookshottransform.LookAt(hookshotposition);

        hookshotsize += hookshotthrowspeed * Time.deltaTime;
        Hookshottransform.localScale = new Vector3(1, 1, hookshotsize);
        if (hookshotsize >= Vector3.Distance(transform.position, hookshotposition))
        {

            cameraFOV.SetCameraFov(hookshotFOV);
            state = State.HookshotFlyinPlayer;
            speedleavesparticles.Play();

        }

    }
    private void stophookshot()
    {
        cameraFOV.SetCameraFov(normalFOV);
        hookshotsize = 1.5f;
        Hookshottransform.localRotation = Quaternion.Euler(0, 0, 0);
        Hookshottransform.localScale = new Vector3(1, 1, hookshotsize);
        speedleavesparticles.Stop();


    }
    private void handlehookshotmovement()
    {
        Hookshottransform.LookAt(hookshotposition);
        float hookshotspeedmin = 10f;

        Vector3 hookshotdir = (hookshotposition - transform.position).normalized;
        float hookshotspeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotposition), hookshotspeedmin, hookshotspeedmax);



        controller.Move(hookshotdir * hookshotspeed * hookshotspeedmultiplier * Time.deltaTime);
        float reachedhookshotpositiondistance = 2f;
        if (Vector3.Distance(transform.position, hookshotposition) < reachedhookshotpositiondistance)
        {
            if (left == true)
            {

                cooldownspamclick2 = true;
                Invoke("resetcooldownspambutton2", cooldownforhookshot);
            }
            if (right == true)
            {

                cooldownspamclick = true;
                Invoke("resetcooldownspambutton", cooldownforhookshot);
            }

            resetgravity();
            state = State.Normal;
            stophookshot();

        }

        if (testhookshotinputleft() && cooldownspamclick2 == false || (testhookshotinputright() && cooldownspamclick == false))
        {


            resetgravity();
            state = State.Normal;
            stophookshot();
            if (left == true)
            {
                cooldownspamclick2 = true;
                Invoke("resetcooldownspambutton2", cooldownforhookshot);
            }
            if (right == true)
            {
                cooldownspamclick = true;
                Invoke("resetcooldownspambutton", cooldownforhookshot);
            }


        }

        if (testjumpinput())
        {
            if (left == true)
            {
                cooldownspamclick2 = true;
                Invoke("resetcooldownspambutton2", cooldownforhookshot);
            }
            if (right == true)
            {
                cooldownspamclick = true;
                Invoke("resetcooldownspambutton", cooldownforhookshot);
            }



            charactervelocitymomentum = hookshotdir * hookshotspeed * momentumextraspeed;
            float jumpspeed = 50f;
            charactervelocitymomentum += Vector3.up * jumpspeed;
            state = State.Normal;
            resetgravity();
            stophookshot();


        }

    }
    void changestatetosimple()
    {


        if (supernormal == true)
        {
            Debug.Log("hier");
            state = State.simple;
            resetgravity();
            stophookshot();
            supernormal = false;

        }
    }
    void changestatetonormal()
    {
        if (normal == true)
        {
            state = State.Normal;
            normal = false;
        }

    }

    void resetcooldownspambutton2()
    {
        cooldownspamclick2 = false;

    }
    void resetcooldownspambutton()
    {
        cooldownspamclick = false;
    }
    void resetrespawn()
    {
        state = State.simple;

        animator.SetTrigger("Load");

        respawn = false;
    }
    void resetcooldown()
    {
        cooldown = false;
    }
    void Grounded()
    {

        grounded = (Physics.CheckSphere(groundcheck.position, grounddistance, groundmask));

    }
    public void mouselooking()
    {

        float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        playercamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }
    private bool testhookshotinputleft()
    {
        return Input.GetMouseButtonDown(1);

    }

    private bool testhookshotinputright()
    {

        return Input.GetMouseButtonDown(0);

    }

    private bool testjumpinput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    private bool testsprintoutput()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }
    private bool testsprintinput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
    private bool testscroll()
    {
        return Input.GetMouseButtonDown(3);
    }


}
