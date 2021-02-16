using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class GameAttributes : MonoBehaviour
{
    //public variables for coins and lifes
    public int coin = 0;
    public static GameAttributes instance;
    public int life = 1;

    // Start is called before the first frame update
    void Start()
    {
        //setting gameatributtes 
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
