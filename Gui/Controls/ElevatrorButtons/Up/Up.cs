using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : guiObject
{
    private ElevatorController elevatorController;

    void Awake()
    {
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    public void UpDown()
    {
        elevatorController.ButtonUp();
    }
}