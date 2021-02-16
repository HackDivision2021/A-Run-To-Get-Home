using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class FloorSetting : MonoBehaviour
{
    public GameObject currentFloor;
    public GameObject nextFloor;


    // Update is called once per frame
    void Update()
    {
        // Manager to Move floors constantly
        if (transform.position.z > currentFloor.transform.position.z+51)
        {
            currentFloor.transform.position = new Vector3(0, 0, nextFloor.transform.position.z + 60);//set next floor to the coordinate
            GameObject temp = currentFloor;
            currentFloor = nextFloor;
            nextFloor = temp;
        }

    }
}
