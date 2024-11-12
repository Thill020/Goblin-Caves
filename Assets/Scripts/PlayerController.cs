using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject playerCamera;

    Rigidbody rb;
    Animator animator;

    [SerializeField]
    float movementSpeed = 10.0f;
    
    [SerializeField]
    [Tooltip("Adjust this value to control the sensitivity of the camera rotation")]
    public float sensitivity = 10.0f;

    private float rotationY = 0.0f;
    private float rotationX = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Initialize the rotation variables to the current rotation
        rotationY = transform.localEulerAngles.y;
        rotationX = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            // Get the mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Adjust the rotation based on the mouse input
            rotationX -= mouseY;
            rotationY += mouseX;

            // Clamp the vertical rotation to avoid flipping
            rotationX = Mathf.Clamp(rotationX, -90, 90);

            // Apply the rotation to the camera
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    private void FixedUpdate()
    {

        // Get the forward direction of the player
        Vector3 playerForward = transform.forward;
        playerForward.y = 0f; // Remove vertical component

        // Move the player based on input and player's forward direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = (horizontalInput * transform.right + verticalInput * playerForward).normalized * movementSpeed * Time.deltaTime;

        // Set the velocity of the Rigidbody directly
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            rb.velocity = movement;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        animator.SetFloat("Player_Velocity", rb.velocity.magnitude);

        // Correct the rotation to be upright
        if (Mathf.Abs(transform.rotation.x) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        }
    }
}
