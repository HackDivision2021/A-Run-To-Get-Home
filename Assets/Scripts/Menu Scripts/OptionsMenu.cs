using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
