using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //traverse through the secene manager 
    }

    // Quit button logic
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();//exit game
        Debug.Log("Quit");
    }
}
