using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChut : MonoBehaviour
{
    public GameObject VFX;
    private AudioSource SFX;
    public AudioClip Boxfall;

    private void Awake()
    {
        SFX = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            VFX.SetActive(true);
            SFX.enabled= true;
            SFX.PlayOneShot(Boxfall);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            VFX.SetActive(false);
            SFX.enabled= false;
        }
    }
}
