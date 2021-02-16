using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip clickClip, mouseOver;
    private AudioSource audioSource;

    private void Start()
    {
        //set soundmanager
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    //play one intance of sound
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }

    //play instance of sound on hover
    public void PlayPointerEnter()
    {
        audioSource.PlayOneShot(mouseOver);
    }
}
