using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class KeyBindManager : MonoBehaviour
{
    //Singleton to access the manager in different parts
    private static KeyBindManager instance;
    private static KeyBindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }
    //Private variables
    private string bindName;

    //Public variables
    public Dictionary<string, KeyCode> KeyBinds { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        KeyBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.UpArrow);
        BindKey("DOWN", KeyCode.DownArrow);
        BindKey("RIGHT", KeyCode.RightArrow);
        BindKey("LEFT", KeyCode.LeftArrow);
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        // If key is not bound already
        if (!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            //Find key bound to that keybind
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            //unbind
            currentDictionary[myKey] = KeyCode.None;          
        }
        //bind
        currentDictionary[key] = keyBind;
        bindName = string.Empty;

    }
}
