using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Obstacle
{
    // For obstacles, if player rolls then do not do damage
    public override void OnTriggerEnter(Collider other)
    {
        if(!PlayerController.instance.isRoll)
            base.OnTriggerEnter(other);
    }
}
