using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Physics values...
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // Camera reference to avoid rotating the player itself (only the camera will rotate).
    [SerializeField] Transform cameraTransform;

    // Controller reference.
    CharacterController controller;

    Vector2 look;
    Vector3 velocity;
    void Start()
    {
        // Hide the mouse cursor when rotating around.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // An awake function is guaranteed to be called before the start function.
    void Awake()
    {
        // Makes sure that the player does not go through objects.
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calling all functions here directly to keep things organized.
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }
    
    void UpdateGravity()
    {
        // Standard gravity formula in Unity3D physics.
        var gravity = Physics.gravity * mass * Time.deltaTime;
        // "isGrounded" helps determine the velocity's y-component, aka the vertical velocity.
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        // Takes as input the horizontal/vertical input from keyboard.
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        // Add a vector pointing forward from player.
        var input = new Vector3();
        
        // W and S key functionality.
        input += transform.forward * y;
        // A and D key functionality.
        input += transform.right * x;

        // Making sure that the player does not move diagonally faster than we move normally.
        input = Vector3.ClampMagnitude(input, 1f);

        // Check if the button for "jump" (default: spacebar) is pressed...
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Add to the y-component of the velocity to lift the player from the ground.
            velocity.y += jumpSpeed;
        }
        
        // Making sure the movement is not frame-rate dependent.
        controller.Move((input * movementSpeed + velocity) * Time.deltaTime);
    }

    void UpdateLook()
    {
        // In each frame, we are going to add mouse input.
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Avoid the camera from vertically rotating 360deg.
        look.y = Mathf.Clamp(look.y, -89f, 89f);

        // Change the rotation of the transform functionality.
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
