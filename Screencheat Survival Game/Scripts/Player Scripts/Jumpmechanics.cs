using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpmechanics : MonoBehaviour
{
    public CharacterController controller; //character controller variable 
    public float speed = 15f; //speed variable 

    Vector3 velocity; //velocity
    public float gravity = -8f; //gravity variable
    public Transform groundCheck; //grounchcheck for player object
    public float groundDistance = 0.4f; //variable for distance to the ground
    public LayerMask groundMask;

    public bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>(); //calling character controller 
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //check if the object is grounded

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //if the object is grounded and velocity is less than 0
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) //jump command
        {
            velocity.y = 5f;
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

    }

}
