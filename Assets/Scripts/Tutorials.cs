using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public GameObject tutorialBg;
    public RawImage tutorialDisplayContainer;
    public Texture slideTutorialTexture, jumpTutorialTexture, dodgeTutorialTexture, policeTutorialTexture;
    public PlayerController playerController;
    public GameObject enemy;
    /// <summary>
    /// prompt tutorial
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "SlideTutorial":
                ShowTutorialImage(slideTutorialTexture);
                break;
            //case "JumpTutorial":
            //    ShowTutorialImage(jumpTutorialTexture);
            //    break;
            case "DodgeTutorial":
                ShowTutorialImage(dodgeTutorialTexture);
                break;
            case "PoliceTutorial":
                ShowTutorialImage(policeTutorialTexture);
                break;
            default:
                break;
        }
    }

    private void ShowTutorialImage(Texture tutrialImage)
    {

        playerController.enabled = false;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<EnemyBehavior>().enabled = false;
        tutorialBg.SetActive(true);
        tutorialDisplayContainer.enabled = true;
        tutorialDisplayContainer.texture = tutrialImage;
        StartCoroutine(ResumeGame());
    }


    IEnumerator ResumeGame()
    {
        yield return new WaitForSeconds(3);
        playerController.enabled = true;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        enemy.GetComponent<EnemyBehavior>().enabled = true;
        tutorialBg.SetActive(false);
        tutorialDisplayContainer.enabled = false;
    }
}
