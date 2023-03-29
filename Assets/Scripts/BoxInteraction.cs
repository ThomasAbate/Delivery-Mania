using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : Interactive
{
    private Rigidbody rb;
    private PlayerInteraction playerInteraction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnInteraction()
    {
        playerInteraction = PlayerInteraction.Instance;
        if (!playerInteraction.isHolding)
        {
            playerInteraction.isHolding = true;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            transform.SetParent(PlayerInteraction.Instance.transform, true);
            transform.position = PlayerInteraction.Instance.holdPosition.transform.position;
            transform.localRotation = Quaternion.identity;
        }
        else if (playerInteraction.isHolding)
        {
            playerInteraction.isHolding = false;
            rb.isKinematic = false;
            rb.detectCollisions = true;
            transform.parent = null;
        }
    }
}
