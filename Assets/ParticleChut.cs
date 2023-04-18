using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChut : MonoBehaviour
{
    public GameObject VFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            VFX.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            VFX.SetActive(false);
        }
    }
}
