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
            AudioClip clip = GetRandomClip();
            SFX.PlayOneShot(clip);
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

    private AudioClip GetRandomClip()
    {
        SFX.volume = Random.Range(0.8f, 1f);
        SFX.pitch = Random.Range(0.95f, 1.05f);
        return Boxfall; 
    }
}
