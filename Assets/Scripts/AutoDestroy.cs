using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class AutoDestroy : MonoBehaviour
{
    public float destroyTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //destroy game object after set time(0.5s)
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
