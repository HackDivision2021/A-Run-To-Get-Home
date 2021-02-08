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
        if (transform.position.z > currentFloor.transform.position.z+37)
        {
            currentFloor.transform.position = new Vector3(0, 0, nextFloor.transform.position.z + 32);
            GameObject temp = currentFloor;
            currentFloor = nextFloor;
            nextFloor = temp;
        }

    }
}
