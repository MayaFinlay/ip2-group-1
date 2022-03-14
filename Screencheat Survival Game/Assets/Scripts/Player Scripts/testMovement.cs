using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // NEW! You must use UnityEngine.InputSystem!

public class testMovement : MonoBehaviour
{
    [Header("Inputs")]
    public Vector2 moveInputs; // X for move left/right, Y for move forward/back
    public Vector2 lookInputs; // X for rotate left/right, Y for look up/down

    [Header("Moving")]
    public Rigidbody playerBody; 
    public float movementSpeed = 25; 
    public float turnSpeed = 100; 

    [Header("Looking")]
    public Transform playerHead;
    public float lookAngleRange = 60;
    private float camRotation = 0;

    public void UpdateMoveInputs(InputAction.CallbackContext context)
    {
        moveInputs = context.ReadValue<Vector2>();
    }

    public void UpdateLookInputs(InputAction.CallbackContext context)
    {
        lookInputs = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Only process if there is input
        if (lookInputs != Vector2.zero)
        {
            // Rotate body on Y axis of player character to turn left/right
            playerBody.transform.Rotate(new Vector3(0, lookInputs.x * turnSpeed * Time.deltaTime), Space.Self);

            // Build up/down rotation over time
            camRotation += lookInputs.y * turnSpeed * Time.deltaTime;

            // Clamp up/down rotation
            camRotation = Mathf.Clamp(camRotation, -lookAngleRange, lookAngleRange);

            // Apply rotation to player's head
            playerHead.localRotation = Quaternion.Euler(-camRotation, 0, 0);

        }
    }

    private void FixedUpdate()
    {
        // Only process if there is input
        if (moveInputs != Vector2.zero)
        {
            playerBody.AddRelativeForce(new Vector3(moveInputs.x * movementSpeed * Time.deltaTime, 0, moveInputs.y * movementSpeed * Time.deltaTime), ForceMode.Impulse);
        }
    }
}
