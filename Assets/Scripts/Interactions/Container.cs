using Leguar.TotalJSON.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class Container : MonoBehaviour
{
    public BoxColor color;
    private Box boxIn;
    private Box boxOut;
    
    private ContainerUI containerUI;

    private void Start()
    {
        containerUI = ContainerUI.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Box>())
        {
            boxIn = other.gameObject.GetComponent<Box>();
            if(boxIn.color == color)
            {
                containerUI.AddToCounter(color, 1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Box>())
        {
            boxOut = other.gameObject.GetComponent<Box>();
            if (boxOut.color == color)
            {
                containerUI.RemoveFromCounter(color, 1);

            }
        }
    }
}
