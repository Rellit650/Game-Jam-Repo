using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls system;

    private Vector2 movement;
    
    private PlayerStateController stateRef;
    private PlayerSplitController splitControllerRef;
    private Rigidbody rb;
    private Vector3 appliedVelocity;
    private Vector3 initialLerpVelocity;
    public float speed;
    public bool slide;
    private float slideTimer;

    private bool stopMoving,
                 firstFrameOfMovement = false;

    private Vector3 cameraDir;
    [SerializeField]
    private float slideTime;
    SplitPickUp pickUpRef;
    float storedY;
    private CinemachineFreeLook cmCamera;

    private AudioSource iceSource,
                        waterSource,
                        gasSource,
                        oldSource,
                        newSource;
    public float newVolume;
    public AudioClip clip;
    public AudioScript handler;
    private void OnEnable()
    {
        system.Enable();
        //cmCamera = FindObjectOfType<CinemachineFreeLook>();
        //Transform obj = gameObject.transform;
        //cmCamera.Follow = obj;
        //cmCamera.LookAt = obj;
    }

    private void OnDisable()
    {
        system.Disable();
    }
    
    private void Awake()
    {
        system = new PlayerControls();
        system.PlayerActions.Move.performed += ctx => OnStartMovePress(ctx.ReadValue<Vector2>());
        system.PlayerActions.Move.canceled += ctx => SetStopBool();
        system.PlayerActions.SwitchState.performed += ctx => SwapState();
        system.PlayerActions.Split.performed += ctx => splitControllerRef.SplitPlayer(gameObject);
        system.PlayerActions.PickUp.performed += ctx => PickUp();
        system.PlayerActions.SwitchPlayer.performed += ctx => SwitchPlayerControl();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stateRef = gameObject.transform.parent.GetComponent<PlayerStateController>();
        splitControllerRef = FindObjectOfType<PlayerSplitController>();
        iceSource = GameObject.Find("IceSource").GetComponent<AudioSource>();
        waterSource = GameObject.Find("WaterSource").GetComponent<AudioSource>();
        gasSource = GameObject.Find("GasSource").GetComponent<AudioSource>();
        handler = GameObject.Find("SwapHandler").GetComponent<AudioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(movement);
        if (stopMoving)
        {
            HandleStopMovement();
        }
        appliedVelocity.x *= speed;
        appliedVelocity.z *= speed;
        storedY = rb.velocity.y;
    }

    void OnStartMovePress(Vector2 ctx)
    {
        movement = ctx; 
        firstFrameOfMovement = true;
    }
    
    private void FixedUpdate()
    {
        Vector3 newVel = new Vector3(appliedVelocity.x, storedY, appliedVelocity.z);
        rb.velocity = newVel;
    }
    void SetStopBool() 
    {
        stopMoving = true;
        movement = Vector2.zero;
    }

    void HandleStopMovement() 
    {
        if (slide)
        {
            //Slide
            slideTimer += Time.deltaTime;
            appliedVelocity = Vector3.Lerp(initialLerpVelocity.normalized, Vector3.zero, slideTimer / slideTime);
        }
        else 
        {
            appliedVelocity = Vector3.zero;
        }
    }

    void MovePlayer(Vector2 movVec)
    {
        if (movement.sqrMagnitude > 0f && rb.velocity.y > -1.0f) 
        {
            stopMoving = false;

            if (firstFrameOfMovement)
            {
                Vector3 dumi = new Vector3(movVec.x, 0f, movVec.y);

                cameraDir = Camera.main.transform.TransformDirection(dumi);
                firstFrameOfMovement = false;
            }

            //We dont want to deal with y movement yet lets just handle our horizontal and vertical for now
            cameraDir.y = 0f;

            //find the angle to rotate our players rotation based on our new direction
            float rotAngle = Mathf.Atan2(cameraDir.x, cameraDir.z) * Mathf.Rad2Deg;

            //rotate our player based on this angle
            gameObject.transform.rotation = Quaternion.Euler(0f, rotAngle, 0f);

            Debug.Log(cameraDir);

            appliedVelocity.z = cameraDir.z;
            appliedVelocity.x = cameraDir.x;
            slideTimer = 0f;
            initialLerpVelocity = appliedVelocity;
            if (!slide)
            {
                appliedVelocity.Normalize();
            }
        }     
    }
    
    public void SetPickUp(SplitPickUp spuRef) 
    {
        pickUpRef = spuRef;
    }

    private void SwapState()
    {
        rb.velocity = Vector3.zero;
        appliedVelocity = Vector3.zero;
        slideTimer = slideTime;
        switch (stateRef.state)
        {
            case 0:
                oldSource = iceSource;
                newSource = waterSource;
                break;
            case 1:
                oldSource = waterSource;
                newSource = gasSource;
                break; 
            case 2:
                oldSource = gasSource;
                newSource = iceSource;
                break;
        }
        handler.SwitchAudioTrackToVariant(oldSource, clip, newVolume, newSource, true, 2.0f);
        stateRef.ChangeToNextState();
    }

    private void PickUp()
    {
        if (pickUpRef != null)
        {
            pickUpRef.PickUp();
        }
    }

    public void SwitchPlayerControl() 
    {
        splitControllerRef.CycleControl();
    }
}
