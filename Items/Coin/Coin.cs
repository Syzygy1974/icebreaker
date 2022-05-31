using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int value;

    public override void ExtendedAwake()
    {
        item.name = "Coin";
        item.type = 1;
        item.value = value;
    }
}
