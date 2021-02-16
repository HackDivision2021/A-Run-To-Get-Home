using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class GameManager : MonoBehaviour
{
    //public variables
    public GameObject resumeButton;
    public GameObject pauseButton;

    //logic for clicking on the pause button
    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0; //time is paused
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    //logic for clicking on the resume button
    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1; //time back to normal
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
