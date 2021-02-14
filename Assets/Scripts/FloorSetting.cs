using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            currentFloor.transform.position = new Vector3(0, 0, nextFloor.transform.position.z + 60);
            GameObject temp = currentFloor;
            currentFloor = nextFloor;
            nextFloor = temp;
        }

    }
}
