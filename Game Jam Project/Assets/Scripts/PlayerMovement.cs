using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Input system
    private PlayerControls system;
    private Vector2 movement;
    
    // Input system booleans
    private bool stopMoving,
                 firstFrameOfMovement;
    
    // Necessary split + state changes
    private PlayerStateController stateRef;
    private PlayerSplitController splitControllerRef;
    
    // Physics :)
    private Rigidbody rb;
    private Vector3 appliedVelocity;
    private Vector3 initialLerpVelocity;
    public float speed;
    public bool slide;
    private float slideTimer;
    private float storedY;

    // Camera-to-player direction variables
    private Vector3 cameraDir;
    private CinemachineFreeLook cmCamera;
    [SerializeField]
    private float slideTime;
    private SplitPickUp pickUpRef;

    // Music stuff
    private AudioSource iceSource,
                        waterSource,
                        gasSource,
                        oldSource,
                        newSource;
    [SerializeField]
    private float newVolume;
    [SerializeField]
    private AudioClip clip;
    private AudioScript handler;
    
    // Input system enable
    private bool isSplit = false;
    private void OnEnable()
    {
        system.Enable();
        //cmCamera = FindObjectOfType<CinemachineFreeLook>();
        //Transform obj = gameObject.transform;
        //cmCamera.Follow = obj;
        //cmCamera.LookAt = obj;
    }

    // Input system disable
    private void OnDisable()
    {
        system.Disable();
    }
    
    // Set up input system listeners
    private void Awake()
    {
        system = new PlayerControls();
        system.PlayerActions.Move.performed += ctx => OnStartMovePress(ctx.ReadValue<Vector2>());
        system.PlayerActions.Move.canceled += ctx => SetStopBool();
        system.PlayerActions.SwitchState.performed += ctx => SwapState();
        system.PlayerActions.Split.performed += ctx => SplitPlayer();
        system.PlayerActions.PickUp.performed += ctx => PickUp();
        system.PlayerActions.SwitchPlayer.performed += ctx => SwitchPlayerControl();
    }

    // Start is called before the first frame update
    private void Start()
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
    private void Update()
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

    // Setting variables for movement directions
    private void OnStartMovePress(Vector2 ctx)
    {
        movement = ctx; 
        firstFrameOfMovement = true;
    }
    
    // Physics :)
    private void FixedUpdate()
    {
        Vector3 newVel = new Vector3(appliedVelocity.x, storedY, appliedVelocity.z);
        rb.velocity = newVel;
    }

    public void SetSplitBool(bool val) 
    {
        isSplit = val;
    }
    void SplitPlayer() 
    {
        if (!isSplit) 
        {
            splitControllerRef.SplitPlayer(gameObject);
        }
    }

    void SetStopBool() 
    {
        stopMoving = true;
        movement = Vector2.zero;
    }

    // Handling movement stop
    private void HandleStopMovement() 
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

    // Updating player position
    void MovePlayer(Vector2 movVec)
    {
        if (!(movement.sqrMagnitude > 0f) || !(rb.velocity.y > -1.0f))
        {
            return;
        }

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

        appliedVelocity.z = cameraDir.z;
        appliedVelocity.x = cameraDir.x;
        slideTimer = 0f;
        initialLerpVelocity = appliedVelocity;
        if (!slide)
        {
            appliedVelocity.Normalize();
        }
    }
    
    // Set which pick up should be obtained
    public void SetPickUp(SplitPickUp spuRef) 
    {
        pickUpRef = spuRef;
    }

    // Swap current state
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

    // Pick up current pick up
    private void PickUp()
    {
        if (pickUpRef != null)
        {
            pickUpRef.PickUp();
        }
    }

    // Switch which split player is controlling
    public void SwitchPlayerControl()
    {
        oldSource = stateRef.state switch
                    {
                        0 => iceSource,
                        1 => waterSource,
                        2 => gasSource,
                        _ => oldSource
                    };
        splitControllerRef.CycleControl(oldSource, handler, 0.5f);
    }
}
