using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : guiObject
{
    private newElevatorController elevatorController;

    void Awake()
    {
        elevatorController = FindObjectOfType<newElevatorController>();
    }

    public void UpDown()
    {
        elevatorController.ButtonUp();
    }
}