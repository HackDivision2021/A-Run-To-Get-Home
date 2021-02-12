using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlayPointerEnter();
    }


    // Play game button logic
    public void PlayGame()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit button logic
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
