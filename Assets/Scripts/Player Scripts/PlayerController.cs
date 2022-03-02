using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController: MonoBehaviour
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
    

    private void Awake()
    {
        moveControls = new PlayerControls();

        moveControls.Gameplay.Movement.performed += ctx => playerMove = ctx.ReadValue<Vector2>();
        moveControls.Gameplay.Movement.canceled += ctx => playerMove = Vector2.zero;
        moveControls.Gameplay.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //get component in the start method
        playerController = GetComponent<CharacterController>(); // Allows player to move through the level
        rb.freezeRotation = true; //freeze the rotation on the rigidbody
    }

    public void Update()
    {
        OnMove();
        Movementadjustor();

    }
    public void OnMove()
    {
        horizontal = playerMove.x;
        vertical = playerMove.y;

        direction = transform.forward * vertical + transform.right * horizontal;
        rb.AddForce(direction.normalized * playerSpeed * speedmultiplier, ForceMode.Acceleration);
       
    }

    void Movementadjustor() //to reduce the "slippery" movement motion
    {
        rb.drag = dragvalue; //assigning a drag value
    }

    public void Jump()
    {
        print("lol you jumped loser");
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
