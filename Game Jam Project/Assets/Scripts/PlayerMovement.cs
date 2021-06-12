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
    [SerializeField]
    float slideTime;
    SplitPickUp pickUpRef;

    private CinemachineFreeLook cmCamera;
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
        system.PlayerActions.Move.canceled += ctx => movement = Vector2.zero;
        system.PlayerActions.Switch.performed += ctx => SwapState();
        system.PlayerActions.Split.performed += ctx => splitControllerRef.SplitPlayer();
        system.PlayerActions.PickUp.performed += ctx => PickUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stateRef = FindObjectOfType<PlayerStateController>();
        splitControllerRef = FindObjectOfType<PlayerSplitController>();
        cmCamera = FindObjectOfType<CinemachineFreeLook>();
        Transform obj = gameObject.transform;
        cmCamera.Follow = obj;
        cmCamera.LookAt = obj;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movement);
        MovePlayer(movement);
    }

    private void FixedUpdate()
    {
        rb.velocity = appliedVelocity * speed;
    }

    void MovePlayer(Vector2 movVec)
    {
        appliedVelocity.z = movVec.y;
        appliedVelocity.x = movVec.x;
        slideTimer = 0f;
        initialLerpVelocity = appliedVelocity;
        
        if (!slide)
        {
            //Stop moving
            appliedVelocity.Normalize();
        }
        else 
        {
            slideTimer += Time.deltaTime;
            appliedVelocity = Vector3.Lerp(initialLerpVelocity.normalized, Vector3.zero, slideTimer / slideTime);
        }
    }
    
    public void SetPickUp(SplitPickUp spuRef) 
    {
        pickUpRef = spuRef;
    }

    private void SwapState()
    {
        rb.velocity = Vector3.zero;
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
