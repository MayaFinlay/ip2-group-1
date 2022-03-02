using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterMovement : MonoBehaviour
{
    [Header("Hunter Movement")]
    // Variables used to set the player's movement speed and character controller
    [SerializeField] private CharacterController charController;
    [SerializeField] private float speed = 2f;

    // Takes in player input controller
    [SerializeField] private Vector2 movementInput;

    public int maxHealth = 100;
    public int currentHealth;

    public float health = 100;
    public GameObject HealthUI;

    private void Start()
    {
        // Take in character controller
        charController = GetComponent<CharacterController>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Update()
    {        
        // Set x and y Movement Inputs respectively 
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        // Set character speed based on direction
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        charController.Move(move * speed * Time.deltaTime);

    }

    public void HealthDamage()
    {
        //runs the damage funtion in this script
        HealthUI.GetComponent<HealthBar>().Damage();
    }

}
