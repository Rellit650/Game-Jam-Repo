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
    private bool stopMoving;
    [SerializeField]
    private float slideTime;
    SplitPickUp pickUpRef;
    float storedY;
    private CinemachineFreeLook camera;
    private void OnEnable()
    {
        system.Enable();
    }

    private void OnDisable()
    {
        system.Disable();
    }
    
    private void Awake()
    {
        system = new PlayerControls();
        system.PlayerActions.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        system.PlayerActions.Move.canceled += ctx => SetStopBool();
        system.PlayerActions.Switch.performed += ctx => SwapState();
        system.PlayerActions.Split.performed += ctx => splitControllerRef.SplitPlayer(gameObject);
        system.PlayerActions.PickUp.performed += ctx => PickUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stateRef = gameObject.transform.parent.GetComponent<PlayerStateController>();
        splitControllerRef = FindObjectOfType<PlayerSplitController>();
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
        Transform obj = gameObject.transform;
        cmCamera.Follow = obj;
        cmCamera.LookAt = obj;
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
        if (movement.sqrMagnitude > 0f) 
        {
            stopMoving = false;
            appliedVelocity.z = movVec.y;
            appliedVelocity.x = movVec.x;
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
        Debug.Log("pick up set");
        pickUpRef = spuRef;
    }

    private void SwapState()
    {
        rb.velocity = Vector3.zero;
        slideTimer = slideTime;
        stateRef.ChangeToNextState();
    }

    private void PickUp()
    {
        if (pickUpRef != null)
        {
            pickUpRef.PickUp();
        }
    }
}
