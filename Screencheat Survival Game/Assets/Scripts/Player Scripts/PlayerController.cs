using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    PlayerControls moveControls;
    Vector2 playerMove;
    Vector2 playerDirection;
    public CharacterController playerController;
    public Camera cameraPlayer;
    public Transform playerBody;

    public float playerSpeed = 5f; //movement speed variable
    float horizontal; //float value for horizontal movement
    float vertical; //float value for vertical movement
    Vector3 direction; //Vector3 to store movement direction.
    Rigidbody rb; //reference to rigidbody
    float dragvalue = 5f; //asigned value for drag 
    public float speedmultiplier = 5f; //multiplier for speed value 

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private PickableItem pickedItem;

    public Transform groundCheck;
    public float groundDistance = 0.8f;
    public LayerMask groundMask;
    public GameObject[] allGameObjects;

    public bool isGrounded;

    private void Awake()
    {
        moveControls = new PlayerControls();

        moveControls.Gameplay.Movement.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        moveControls.Gameplay.Movement.canceled += ctx => playerMove = Vector2.zero;
        moveControls.Gameplay.Jump.performed += ctx => Jump();

        //lazy way to avoid stacking each other when starting the game
        allGameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (allGameObjects.Length == 1)
        {
            this.transform.position = new Vector3(-5, 1, 4);
        }

        if (allGameObjects.Length == 2)
        {
            this.transform.position = new Vector3(5, 1, 4);
        }
    }

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>(); //get component in the start method
        playerController = GetComponentInChildren<CharacterController>(); // Allows player to move through the level
        rb.freezeRotation = true; //freeze the rotation on the rigidbody
    }

    public void Update()
    {
        // Checks to see if player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        OnMove();
        Movementadjustor();
        Collect();

    }


    public void OnMove()
    {
        horizontal = playerMove.x;
        vertical = playerMove.y;

        direction = transform.forward * vertical + transform.right * horizontal;
        rb.AddForce(direction.normalized * playerSpeed * speedmultiplier, ForceMode.Acceleration);
        Movementadjustor();
    }
    void Movementadjustor() //to reduce the "slippery" movement motion
    {
        rb.drag = dragvalue; //assigning a drag value
    }



    public void Jump()
    {
        print("lol you jumped loser");
    }

    public void Collect()
    {
        // Check if player picked some item already
        if (pickedItem)
        {
            // If yes, drop picked item
            DropItem(pickedItem);
        }
        else
        {
            // If no, try to pick item in front of the player
            // Create ray from center of the screen
            var ray = cameraPlayer.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            // Shot ray to find object to pick
            if (Physics.Raycast(ray, out hit, 1.5f))
            {
                // Check if object is pickable
                var pickable = hit.transform.GetComponent<PickableItem>();
                // If object has PickableItem class
                if (pickable)
                {
                    // Pick it
                    PickItem(pickable);
                }
            }
        }
    }

    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>


    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision col)
    {

        float timerWall = 0f;
        bool timerStart;

;
        if (col.gameObject.tag == "Ghost Wall")
        {
            col.gameObject.SetActive(false);
            timerStart = true;
        
        
            if (timerStart == true)
            {
                timerWall += Time.deltaTime;
            }
            
           
            if(timerWall == 2f)
            {
                timerStart = false;
                timerWall = 0f;
                col.gameObject.SetActive(true);
            }
        
       }
    }


    void OnEnable()
    {
        moveControls.Gameplay.Enable();
    }

    void OnDisable()
    {
        moveControls.Gameplay.Disable();
    }
}
