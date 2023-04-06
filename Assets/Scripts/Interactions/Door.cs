using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactive
{
    public override void OnInteraction()
    {
        Debug.Log("bonjour");
        transform.Rotate(transform.GetChild(0).position, 90f);
    }
}
