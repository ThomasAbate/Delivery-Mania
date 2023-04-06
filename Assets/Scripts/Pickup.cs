using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void OnInteraction()
    {
        if (PlayerInteraction.Instance.heldObject != null)
        {
            DropPhysics();
            PlayerInteraction.Instance.heldObject = null;
            PlayerInteraction.Instance.heldObjectRb = null;
        }
        else
        {
            PickupPhysics();
            gameObject.GetComponent<Outline>().enabled = false;
            PlayerInteraction.Instance.heldObject = gameObject;
            PlayerInteraction.Instance.heldObjectRb = gameObject.GetComponent<Rigidbody>();
        }
    }

    private void PickupPhysics()
    {
        rb.useGravity = false;
        rb.drag = 10;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.SetParent(PlayerInteraction.Instance.transform, true);
        transform.position = PlayerInteraction.Instance.holdArea.transform.position;
        transform.localRotation = Quaternion.identity;
    }

    private void DropPhysics()
    {
        rb.useGravity = true;
        rb.drag = 1;
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
    }

    public void DisableInteraction()
    {
        this.enabled = false;
    }
}