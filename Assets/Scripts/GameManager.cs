using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject pauseButton;

    //logic for clicking on the pause button
    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    //logic for clicking on the resume button
    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
