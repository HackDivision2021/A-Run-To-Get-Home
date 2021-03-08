using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/***
 * This class is for destination
 */
public class Destination : MonoBehaviour
{
    public static Destination instance;
    public bool isWin;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
