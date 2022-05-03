using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashAmount : guiObject
{
    public TextMeshProUGUI displayText;

    public override void LocalAwake()
    {
        displayText = GetComponent<TextMeshProUGUI>();
    }

    public void IncCashAmount (int amount) 
    {
        Debug.Log ("IncCashAmount: " + amount);
        displayText.text = amount.ToString();
    }
}
