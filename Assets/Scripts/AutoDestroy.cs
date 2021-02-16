using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * This class is for auto destory GO in a specified time.
 */
public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
