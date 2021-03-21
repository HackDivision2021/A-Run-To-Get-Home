using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/***
 * This class is for UI control
 */
public class GameManager : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject pauseButton;
    public GameObject loadButton;
    public GameObject saveButton;
    public GameObject exitButton;
    public GameObject inventoryPanel;
    private bool isInventoryPanelOpen;
    public GameObject player;
    public GameObject savingText;
    public GameObject enemies;

    [Header("PLayer Settings")]
    public PlayerController myPlayer;

    [Header("Scene Data")]
    public SceneDataSO sceneData;

    //logic for clicking on the pause button
    public void OnPauseButtonClicked()
    {
        //Time.timeScale = 0;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        loadButton.SetActive(true);
        saveButton.SetActive(true);
        exitButton.SetActive(true);
        
        player.GetComponent<Animation>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        enemies = GameObject.Find("Enemies");
        enemies.SetActive(false);


    }

    //logic for clicking on the resume button
    public void OnResumeButtonClicked()
    {
        //Time.timeScale = 1;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        loadButton.SetActive(false);
        saveButton.SetActive(false);
        exitButton.SetActive(false);
        player.GetComponent<Animation>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        enemies.SetActive(true);

    }

    public void OnExitButtonClicked()
    {
        //SoundManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Scene current = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        // press tab to open inventory panel
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryButonClicked();
        }
    }

    // when inventory icon button clicked
    public void OnInventoryButonClicked()
    {
        isInventoryPanelOpen = !isInventoryPanelOpen;
        inventoryPanel.SetActive(isInventoryPanelOpen);
    }

    //Load Game Method
    public void LoadGame()
    {
        myPlayer = FindObjectOfType<PlayerController>();

        myPlayer.controller.enabled = false;
        myPlayer.transform.position = sceneData.playerPosition;
        myPlayer.controller.enabled = true;

        myPlayer.health = sceneData.playerHealth;
        myPlayer.SetHealth(sceneData.playerHealth);
    }

    //Save Game method
    public void SaveGame()
    {
        // 1. show the saving text for 2 seconds
        StartCoroutine(ShowSavingText());

        myPlayer = FindObjectOfType<PlayerController>();

        sceneData.playerPosition = myPlayer.transform.position;
        sceneData.playerHealth = myPlayer.health;


        // 2. save game
        //Save save = CreateSaveGO();
        //string saveJson = JsonUtility.ToJson(save);
        //Debug.Log(saveJson);
        //// using binary to save game
        //FileStream fs = new FileStream(Application.dataPath + "/save.txt", FileMode.Create);
        //byte[] bytes = new UTF8Encoding().GetBytes(saveJson.ToString());
        //fs.Write(bytes, 0, bytes.Length);

        //fs.Close();

        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.dataPath + "/gamesave.save");
        //bf.Serialize(file, save);
        //file.Close();

    }

    IEnumerator ShowSavingText()
    {
        savingText.SetActive(true);
        saveButton.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(1);
        savingText.SetActive(false);
        saveButton.GetComponent<Button>().enabled = true;
    }

    /// <summary>
    /// saving game
    /// </summary>
    /// <returns></returns>
    private Save CreateSaveGO()
    {
        // 1. create save obj
        Save save = new Save();
        // 2. save the number of coins
        save.coinNum = GameAttributes.instance.CoinCount;
        // 3. save the number of diamonds
        save.diamondNum = GameAttributes.instance.DiamondCount;
        // 4. save the position of player
        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.playerPositionZ = player.transform.position.z;

        return save;
    }
}
