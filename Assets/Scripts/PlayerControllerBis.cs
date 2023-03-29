using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerBis : MonoBehaviour
{
    public static PlayerControllerBis instance;

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

    // Start is called before the first frame update
    void Start()
    {
        isCarton = false;
        isDepot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.y != 0)
        {
            transform.position += transform.forward * direction.y * moveSpeed * Time.deltaTime;
        }
        if (direction.x != 0)
        {
            transform.position += transform.right * direction.x * moveSpeed * Time.deltaTime;
        }

    }

    

    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if(isCarton && freeHand)
            {
                freeHand = false;
            }
            else if(isDepot && !freeHand)
            {
                //Poser le carton
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carton")
        {
            isCarton = true;
        }
    }
}
