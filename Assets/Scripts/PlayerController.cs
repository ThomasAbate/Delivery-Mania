using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private CharacterController controller;
    public bool lockMovements;

    [Header("Movement Settings")]
    [Space]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprint = 5f;
    [SerializeField] private float moveSmoothTime = 0.2f;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    private bool isSprinting;
    private float speedValue;

    [Header("Jump Settings")]
    [Space]
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float jumpHoldTime = 0.2f;
    [SerializeField] private float jumpTimer;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private bool isJumping;
    private float verticalVelocity;


    Vector2 movement;
    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        controller = GetComponent<CharacterController>();
        speedValue = speed;
    }

    void Update()
    {
        if(!lockMovements)
        {
            float x = movement.x;
            float z = movement.y;

            CheckSprint();

            Vector3 move = transform.right * x + transform.forward * z;
            currentMoveVelocity = Vector3.SmoothDamp(
                currentMoveVelocity,
                move * speedValue,
                ref moveDampVelocity,
                moveSmoothTime);
            controller.Move(currentMoveVelocity * Time.deltaTime);

            CheckJump();
        }
    }

    void CheckSprint()
    {
        if(movement.x < 0.05f && movement.y < 0.05f)
        {
            isSprinting = false;
        }
        if(isSprinting)
        {
            speedValue = speed + sprint;
        }
        else
        {
            speedValue = speed;
        }
    }
    void CheckJump()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float jumpHeight = Mathf.Clamp01(jumpTimer / jumpHoldTime);
            verticalVelocity = jumpSpeed * jumpHeight;
            if (verticalVelocity >= jumpSpeed)
                isJumping = false;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null && !rb.isKinematic)
        {
            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            rb.velocity = pushDirection * 10f;
        }
    }

    #region Input

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started) { 
            if (controller.isGrounded)
            {
                isJumping = true;
                jumpTimer = 0.0f;
            }
        }
        else if (context.canceled)
        {
            isJumping = false;
            jumpTimer = 0;
        }

    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(isSprinting)
            {
                isSprinting = false;
            }
            else
            {
                isSprinting = true;
            }
        }
    }

    #endregion
}
