using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TMPro.TextMeshProUGUI right, left, down;
    public GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Right", KeyCode.D);
        keys.Add("Left", KeyCode.A);
        keys.Add("Down", KeyCode.S);

        right.text = keys["Right"].ToString();
        left.text = keys["Left"].ToString();
        down.text = keys["Down"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //Pressing a key
        if(currentKey != null)
        {
            //Assigning key
            Event e = Event.current;
            if (e.isKey)
            {
                //Change key
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
        Debug.Log("Key changed");
    }
}
