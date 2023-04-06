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
    private int boxCount = 0;

    [SerializeField] private GameObject textObject;
    private TextMeshPro counter;

    private void Start()
    {
        counter = textObject.GetComponent<TextMeshPro>();
        counter.text = "0";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Box>())
        {
            boxIn = other.gameObject.GetComponent<Box>();
            if(boxIn.color == color)
            {
                boxCount++;
                counter.text = boxCount.ToString();
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
                boxCount--;
                counter.text = boxCount.ToString();
            }
        }
    }
}
