using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterLook : MonoBehaviour
{
    Vector2 cameraMove;
    PlayerControls lookControls;
    [SerializeField]
    float cameraSensitivity = 100f;
    public Camera cameraPlayer;
    float xRotation = 0f;

    public void Awake()
    {
        lookControls = new PlayerControls();

        lookControls.Gameplay.Look.performed += ctx => cameraMove += ctx.ReadValue<Vector2>();
    }

    public void Update()
    {
        OnLook();
    }



    public void OnLook()
    {
        cameraPlayer = new Camera();

        float rightStickX = cameraMove.x * cameraSensitivity * Time.deltaTime;
        print(rightStickX);
        float rightStickY = cameraMove.y * cameraSensitivity * Time.deltaTime;
        print(rightStickY);

        // Keeps body from rotating trough Z axis
        xRotation -= rightStickY;

        //Lets body rotate through X axis
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Processes rotation into angles
        transform.localRotation = Quaternion.Euler(cameraMove.x, 0f, 0f);

        //Rotates body through X axis
        //body.Rotate(Vector3.up * rightStickX);

    }
    void OnEnable()
    {
        lookControls.Gameplay.Enable();
    }

    void OnDisable()
    {
        lookControls.Gameplay.Disable();
    }
}
