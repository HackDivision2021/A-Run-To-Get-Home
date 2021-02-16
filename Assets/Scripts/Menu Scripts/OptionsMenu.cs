using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    //Logic for the volume slider
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolSlider", volume); //Moving a floating value on the slider
    }

    // Logic for fullscreen toggle
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
