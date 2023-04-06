using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxColor
{
    Default,
    Yellow,
    Green,
    Red
}

public class Box : Pickup
{
    public BoxColor color = BoxColor.Default;

    public override void OnInteraction()
    {
        base.OnInteraction();
    }
}
