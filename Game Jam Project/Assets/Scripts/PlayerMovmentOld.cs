using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentOld : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stateRef = FindObjectOfType<PlayerStateController>();
        splitControllerRef = FindObjectOfType<PlayerSplitController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Controls
        if (Input.GetKey(KeyCode.A)) 
        {
            appliedVelocity.x = -1.0f;
            slideTimer = 0f;
            initialLerpVelocity = appliedVelocity;
        }
        if (Input.GetKey(KeyCode.S))
        {
            appliedVelocity.z = -1.0f;
            slideTimer = 0f;
            initialLerpVelocity = appliedVelocity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            appliedVelocity.x = 1.0f;
            slideTimer = 0f;
            initialLerpVelocity = appliedVelocity;
        }
        if (Input.GetKey(KeyCode.W))
        {
            appliedVelocity.z = 1.0f;
            slideTimer = 0f;
            initialLerpVelocity = appliedVelocity;
        }
        
        if (!slide)
        {
            //Stop moving
            if (Input.GetKeyUp(KeyCode.A))
            {
                appliedVelocity.x = 0;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                appliedVelocity.z = 0;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                appliedVelocity.x = 0;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                appliedVelocity.z = 0;
            }
            appliedVelocity.Normalize();
        }
        else 
        {
            
            slideTimer += Time.deltaTime;
            appliedVelocity = Vector3.Lerp(initialLerpVelocity.normalized, Vector3.zero, slideTimer / slideTime);
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            splitControllerRef.SplitPlayer();
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (pickUpRef != null) 
            {
                pickUpRef.pickUp();
            }         
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.velocity = Vector3.zero;
            stateRef.ChangeToNextState();
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = appliedVelocity * speed;
    }

    public void SetPickUp(SplitPickUp spuRef) 
    {
        pickUpRef = spuRef;
    }
}
