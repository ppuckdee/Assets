using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float moveSpeed = 5f;

    public Transform playerBody;

    float xRotation = 0f;

    private Rigidbody rb;

    public Camera playerCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }

    void Update()
    {
        // Handle Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }

        // Only proceed with rotation and movement if the cursor is locked
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Rotation
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotate the camera vertically
            transform.Rotate(Vector3.up * mouseX);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the player body horizontally
            playerBody.Rotate(Vector3.up * mouseX);

            // Movement
            // Get input from the player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Get the forward and right vectors of the camera
            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;

            // Project the vectors onto the horizontal plane (y = 0)
            forward.y = 0f;
            right.y = 0f;

            // Normalize the vectors to ensure consistent speed in all directions
            forward.Normalize();
            right.Normalize();

            // Calculate the movement direction
            Vector3 movement = (forward * verticalInput + right * horizontalInput);

            // Move the player using Rigidbody
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }

    void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}

