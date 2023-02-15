using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    #region Player Information
    [Space]
    [Header("Movement")]
    [Space]
    public float moveSpeed, jumpForce;
    public Rigidbody rb;

    [Space]
    [Header("Bool")]
    [Space]
    public bool isCarton, isDepot, isGrounded;
    [SerializeField] public bool freeHand;

    #endregion

    #region Private Information
    private Vector2 direction;
    #endregion


    private void Start()
    {
        isCarton = false;
        isDepot = false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = moveSpeed * Time.deltaTime * new Vector3(direction.x, 0, direction.x);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    #region Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
    #endregion

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed && isCarton && freeHand)
        {
            freeHand = false;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carton")
        {
            isBed = true;
            currentBed = collision.transform;
        }
}
