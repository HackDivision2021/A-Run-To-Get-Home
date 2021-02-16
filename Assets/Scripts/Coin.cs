using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class Coin : MonoBehaviour
{
    public float rotateSpeed = 1;
    public GameObject hitEffect;
    public AudioClip hitClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate coins constantly
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    //when player comes in contact with coins
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //add coins
            GameAttributes.instance.coin++;
            AudioSource.PlayClipAtPoint(hitClip, this.transform.position); //play sound

            GameObject hitEffectGO = Instantiate(hitEffect, this.transform.position, Quaternion.identity);//instantiate visual effect
            //destoy the coins
            Destroy(this.gameObject);
        }
    }
}
