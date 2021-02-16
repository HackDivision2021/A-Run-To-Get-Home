using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pablo Saldarriaga ID: 301092976
 * Cong Wang ID: 301098547
 * Xavier de Moraes Batista, Arthur Ivson id: 301063251
 */
public class InventoryOpener : MonoBehaviour
{

    public GameObject Panel;

    //Open inventory panel logic
    public void OpenPanel()
    {
        bool isActive = Panel.activeSelf;

        Panel.SetActive(!isActive);
    }
}
