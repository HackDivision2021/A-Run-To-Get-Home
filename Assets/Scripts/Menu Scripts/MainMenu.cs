using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler
{

    public PlayerController myPlayer;
    public CharacterController controller;
    public SceneDataSO sceneData;

    private void Start()
    {
    }

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


    public void LoadGame()
    {
        SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Scene current = SceneManager.GetActiveScene();

        SingletonPersistentManager.Instance.value++;

        myPlayer = FindObjectOfType<PlayerController>();

        myPlayer.controller.enabled = false;
        myPlayer.transform.position = sceneData.playerPosition;
        myPlayer.controller.enabled = true;

        myPlayer.health = sceneData.playerHealth;
        myPlayer.SetHealth(sceneData.playerHealth);
    }

    // Quit button logic
    public void QuitGame()
    {
        SoundManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}
