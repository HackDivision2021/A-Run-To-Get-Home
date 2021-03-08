using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***
 * This class is to record the number of collectives and life
 */

public class GameAttributes : MonoBehaviour
{
    private int _coinCount;
    private int diamondCount;

    public int CoinCount { get; set; }
    public int DiamondCount { get; set; }

    public static GameAttributes instance;
    public int life = 5;

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
