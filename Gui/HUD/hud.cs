using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hud : guiObject
{
    private GameObject goCashAmount;
    private CashAmount cashAmount;

    public override void LocalAwake()
    {
        goCashAmount = GameObject.Find("CashAmount");
        cashAmount = goCashAmount.GetComponent<CashAmount>();
    }

    public void IncCashAmount (int amount) {
        cashAmount.IncCashAmount(amount);
    }
}
