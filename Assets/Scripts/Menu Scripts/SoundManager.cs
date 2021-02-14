using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip clickClip, mouseOver;
    private AudioSource audioSource;

    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }

    public void PlayPointerEnter()
    {
        audioSource.PlayOneShot(mouseOver);
    }
}
