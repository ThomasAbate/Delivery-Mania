using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public InteractionType interactionType = InteractionType.None;

    public bool onlyOnce = true;

    public virtual void OnInteraction()
    {
        Debug.LogWarning("This interaction has not been coded yet !");
    }
}