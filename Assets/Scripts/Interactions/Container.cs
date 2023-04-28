using Leguar.TotalJSON.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public BoxColor color;
    private Box boxIn;
    private Box boxOut;
    
    private Score score;

    private void Start()
    {
        score = Score.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Box>())
        {
            boxIn = other.gameObject.GetComponent<Box>();
            if(boxIn.color == color)
            {
                score.AddToCounter(color, 1);
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
                score.RemoveFromCounter(color, 1);

            }
        }
    }
}
