using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public override void ExtendedAwake()
    {
        item.name = "Key";
        item.type = 2;
        item.value = 0;
    }
}
