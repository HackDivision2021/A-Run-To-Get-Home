using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameAttributes.instance.coin++;
            AudioSource.PlayClipAtPoint(hitClip, this.transform.position);

            GameObject hitEffectGO = Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
