using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    public static LookWithMouse Instance;

    const float k_MouseSensitivityMultiplier = 0.01f;
    const float k_GamepadSensitivityMultiplier = 0.01f;

    public PlayerInput playerInput;
    public float mouseSensitivity = 100f;
    public float gamepadSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    [SerializeField] private bool invertYAxis;
    [SerializeField][Range(-90.0f, 0)] private float minAngle = -90f;
    [SerializeField][Range(0, 90.0f)] private float maxAngle = 90f;


    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        bool unlockPressed = false, lockPressed = false;

        float mouseX = 0, mouseY = 0;

        if (Mouse.current != null)
        {
            var delta = Mouse.current.delta.ReadValue() / 15.0f;
            mouseX += delta.x;
            mouseY += delta.y;
            lockPressed = Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame;
        }
        if (Gamepad.current != null)
        {
            var value = Gamepad.current.rightStick.ReadValue() * 2;
            mouseX += value.x;
            mouseY += value.y;
        }
        if (Keyboard.current != null)
        {
            unlockPressed = Keyboard.current.escapeKey.wasPressedThisFrame;
        }

        if (playerInput.currentControlScheme == "Gamepad")
        {
            mouseX *= gamepadSensitivity * k_GamepadSensitivityMultiplier;
            mouseY *= gamepadSensitivity * k_GamepadSensitivityMultiplier;
        }
        else
        {
            mouseX *= mouseSensitivity * k_MouseSensitivityMultiplier;
            mouseY *= mouseSensitivity * k_MouseSensitivityMultiplier;
        }

        if (unlockPressed)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (lockPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);

            int invertAxis = invertYAxis ? -1 : 1;
            transform.localRotation = Quaternion.Euler(invertAxis * xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
