using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class Board : Obstacle
{
    // For obstacles, if player rolls then do not do damage
    public override void OnTriggerEnter(Collider other)
    {
        if(!PlayerController.instance.isRoll)
            base.OnTriggerEnter(other);
    }
}
