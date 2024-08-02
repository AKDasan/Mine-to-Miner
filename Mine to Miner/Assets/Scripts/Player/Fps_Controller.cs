using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps_Controller : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation = 0.0f;

    private bool isCanvasOpen = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        CanvasController.OnCanvasToggle += HandleCanvasToggle;
    }

    private void OnDisable()
    {
        CanvasController.OnCanvasToggle -= HandleCanvasToggle;
    }

    private void HandleCanvasToggle(bool isOpen)
    {
        isCanvasOpen = isOpen;
    }

    private void CursorLockController()
    {
        if (isCanvasOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (!isCanvasOpen)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            transform.Rotate(0, mouseX, 0);

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        CursorLockController();
        
        float moveForwardBackward = Input.GetAxis("Vertical") * speed;
        float moveLeftRight = Input.GetAxis("Horizontal") * speed;

        Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBackward;

        controller.Move(move * Time.deltaTime);

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, controller.height / 2 + 0.1f);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
