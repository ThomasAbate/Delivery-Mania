using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlèchePop : MonoBehaviour
{
    public GameObject Fleche;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(Fleche);
    }
}
