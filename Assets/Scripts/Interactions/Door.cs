using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactive
{
    private bool open;
    [SerializeField] private bool openSide;
    public override void OnInteraction()
    {
        if (!open)
        {
            if(openSide)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            open = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            open = false;
        }
    }
}
