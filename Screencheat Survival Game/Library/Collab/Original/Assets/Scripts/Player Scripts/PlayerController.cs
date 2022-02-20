using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController: MonoBehaviour
{
    public float speed = 5f; //movement speed variable
    Vector3 direction; //Vector3 to store movement direction.
    float horizontal; //float value for horizontal movement
    float vertical; // float value for vertical movement
    Rigidbody rb; //reference to rigidbody
    float dragvalue = 5f; //asigned value for drag 
    public float speedmultiplier = 5f; //multiplier for speed value 

    PlayerControls controls;
    Vector2 cameraMove;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Look.performed += ctx => cameraMove += ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => cameraMove = Vector2.zero;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //get component in the start method
        rb.freezeRotation = true; //freeze the rotation on the rigidbody
    }

    private void Update()
    {
        PlayerInput(); //method to handle input and call it in update
        movementadjustor();

        Vector2 m = new Vector2(cameraMove.x, cameraMove.y) * Time.deltaTime;
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //horizontal movement
        vertical = Input.GetAxisRaw("Vertical"); //vertical movement
        direction = transform.forward * vertical + transform.right * horizontal; // transform.forward and transform.right are used to move in the direction relative to where the player is looking
    }
    void movementadjustor() //to reduce the "slippery" movement motion
    {
        rb.drag = dragvalue; //assigning a drag value
    }
    private void FixedUpdate() //fixed update allows the frequency of the physics system required by the rigidbody
    {
        playermv(); //method for player movement called in fixedUpdate
    }
    void playermv()
    {
        rb.AddForce(direction.normalized * speed * speedmultiplier, ForceMode.Acceleration);// adding a force to the rigidbody while normalizing the movement of the player
    }
}
