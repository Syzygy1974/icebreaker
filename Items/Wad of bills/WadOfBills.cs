using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WadOfBills : Item
{
    public override void ExtendedAwake()
    {
        item.name = "Wad of bills";
        item.type = 1;
        item.value = 10000;
    }
}
