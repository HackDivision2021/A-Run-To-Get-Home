using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    [Header("Sound Effect")]
    //private AudioSource audioSource;
    public AudioClip diamondClip;

    [Header("Particle Sytem")]
    public GameObject diamondParticle;

    [Header("Roation")]
    [Range(0, 300)] public int rotateSpeed;


    // Update is called once per frame
    void Update()
    {
        //audioSource = GetComponent<AudioSource>();
        this.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. play sound
        AudioSource.PlayClipAtPoint(diamondClip, Camera.main.transform.position);
        // 2. instantiate particle system
        GameObject particle = Instantiate(diamondParticle, this.transform.position, Quaternion.identity);
        // 3. destory object
        this.gameObject.SetActive(false);
        Destroy(this, 2);
        // 4. destory particle system
        Destroy(particle, 0.5f);
        // 5. store the number of the object
        GameAttributes.instance.DiamondCount++;
    }
}
