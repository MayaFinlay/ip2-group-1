using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterLook : MonoBehaviour
{
    PlayerControls controls;
    Vector2 cameraMove;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Look.performed += ctx => cameraMove += ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => cameraMove = Vector2.zero;
    }

    void Update()
    {
        Vector2 m = new Vector2(cameraMove.x, cameraMove.y) * Time.deltaTime;
    }
    
}
