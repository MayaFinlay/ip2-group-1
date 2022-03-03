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

    public void Awake()
    {
        lookControls = new PlayerControls();

        lookControls.Gameplay.Look.performed += ctx => cameraMove += ctx.ReadValue<Vector2>();
        lookControls.Gameplay.Look.canceled += ctx => cameraMove = Vector2.zero;
    }

    public void Update()
    {
        OnLook();
    }



    public void OnLook()
    {
        cameraPlayer = new Camera();

        float rightStickX = cameraMove.x * cameraSensitivity * Time.deltaTime;
        float rightStickY = cameraMove.y * cameraSensitivity * Time.deltaTime;

        // Processes rotation into angles
        transform.localRotation = Quaternion.Euler(cameraMove.x, 0f, 0f);


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
