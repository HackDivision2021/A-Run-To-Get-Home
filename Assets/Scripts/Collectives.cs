using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectives : MonoBehaviour
{
    public static Collectives instance;
    private int _coinCount;
    private int diamondCount;

    public int CoinCount { get; set; }
    public int DiamondCount { get; set; }

    private void Awake()
    {
        instance = this;
    }

    public void CoinIncrease()
    {
        CoinCount++;
    }

    public void DiamondIncrease()
    {
        DiamondCount++;
    }


}
