using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * This class is for coins
 */
public class Coin : MonoBehaviour
{
    public float rotateSpeed = 1;
    public GameObject hitEffect;
    public AudioClip hitClip;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, 0);
    }

    // handle trigger enter event
    private void OnTriggerEnter(Collider other)
    {
        // if the tag of collided GO is "Player"
        if(other.CompareTag("Player"))
        {
            GameAttributes.instance.CoinCount++;
            AudioSource.PlayClipAtPoint(hitClip, this.transform.position);

            GameObject hitEffectGO = Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
