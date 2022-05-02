using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override void ExtendedAwake()
    {
        item.name = "Coin";
        item.type = 1;
        item.value = 1;
    }
}
