using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***
 * This class is to record the number of collectives and life
 */

public class GameAttributes : MonoBehaviour
{
    public int coin = 0;
    public static GameAttributes instance;
    public int life = 1;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
