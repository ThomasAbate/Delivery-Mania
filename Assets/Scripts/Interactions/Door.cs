using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Door : Interactive
{
    private bool open;
    [SerializeField] private bool openSide;

    AudioSource Start;

    private void Awake()
    {
        Start= GetComponent<AudioSource>();
    }
    public override void OnInteraction()
    {
        if (!GameTimer.Instance.startGame)
        {
            GameTimer.Instance.StartTimer();
            Start.enabled= true;
        }
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
