using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TMPro.TextMeshProUGUI right, left, down;

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
}
