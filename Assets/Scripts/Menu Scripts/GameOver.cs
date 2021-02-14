using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //play again button
    public void PlayAgain()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //back to menu button
    public void BackToMenu()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //quit game button
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
