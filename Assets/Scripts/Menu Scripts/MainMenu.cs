using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler
{
    //sound to trigger on button
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlayPointerEnter();
    }


    // Play game button logic
    public void PlayGame()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Scene current = SceneManager.GetActiveScene();
    }

    // Quit button logic
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
