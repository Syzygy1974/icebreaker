using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    private TextMeshPro displayText;
    // Update is called once per frame
    void Start()
    {
        displayText = GetComponent<TextMeshPro>();
        // controllerRigidbody = GetComponent<Rigidbody2D>();
        displayText.text = "";
        // GetComponent<TextMeshPro>().text = "Hello World";
    }

    public void DisplayText (string text) 
    {
        displayText.text = text;
    }

}
