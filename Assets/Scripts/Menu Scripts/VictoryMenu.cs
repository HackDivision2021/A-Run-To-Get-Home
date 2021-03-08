using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //back to menu button
    public void BackToMenu()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    //quit game button
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
