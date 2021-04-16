using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{

    public Image achievement;
    public Sprite coin5, coin20, diamond1, uselessWork;

    // Update is called once per frame
    void Update()
    {
        switch (GameAttributes.instance.CoinCount)
        {
            case 5:
                DisplayAchievement(coin5);
                break;
            case 20:
                DisplayAchievement(coin20);
                break;
            default:
                break;
        }

        switch (GameAttributes.instance.DiamondCount)
        {
            case 1:
                DisplayAchievement(diamond1);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("uselessWorkAchievement"))
        {
            DisplayAchievement(uselessWork);
        }
    }

    private void DisplayAchievement(Sprite achievementImage)
    {
        achievement.sprite = achievementImage;
        achievement.enabled = true;
        StartCoroutine(achievementVanish());
    }

    IEnumerator achievementVanish()
    {
        yield return new WaitForSeconds(3);
        achievement.enabled = false;
    }
}
