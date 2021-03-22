using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPersistentManager : MonoBehaviour
{
    //singleton pattern to keep persistent data for enemies
    public static SingletonPersistentManager Instance { get; private set; }
    public int value;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
