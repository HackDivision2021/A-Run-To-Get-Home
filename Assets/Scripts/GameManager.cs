using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/***
 * This class is for UI control
 */
public class GameManager : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject pauseButton;
    public GameObject saveButton;
    public GameObject inventoryPanel;
    private bool isInventoryPanelOpen;
    public GameObject player;
    public GameObject savingText;

    //logic for clicking on the pause button
    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0;
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        saveButton.SetActive(true);
    }

    //logic for clicking on the resume button
    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1;
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        saveButton.SetActive(false);
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

    /// <summary>
    /// save button cliced, save game info
    /// </summary>
    public void SaveGame()
    {
        // 1. show the saving text for 2 seconds
        StartCoroutine(ShowSavingText());

        //// 2. save game
        //Save save = CreateSaveGO();
        //// using binary to save game
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream fileStream = File.Create(Application.dataPath + "/StreamingFile" + "/save_game.txt");

        //bf.Serialize(fileStream, save);
        //fileStream.Close();

    }

    IEnumerator ShowSavingText()
    {
        savingText.SetActive(true);
        yield return new WaitForSeconds(1);
        savingText.SetActive(false);
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
        save.coinNum = Collectives.instance.CoinCount;
        // 3. save the number of diamonds
        save.diamondNum = Collectives.instance.DiamondCount;
        // 4. save the position of player
        save.playPosition = player.transform.position;

        return save;
    }
}
