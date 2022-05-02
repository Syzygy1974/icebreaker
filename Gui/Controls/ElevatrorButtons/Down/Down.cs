using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : guiObject
{
    private newElevatorController elevatorController;

    void Awake()
    {
        elevatorController = FindObjectOfType<newElevatorController>();
    }

    public void DownDown()
    {
        elevatorController.ButtonDown();
    }
}