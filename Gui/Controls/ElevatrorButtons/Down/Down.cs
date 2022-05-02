using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : guiObject
{
    private ElevatorController elevatorController;

    void Awake()
    {
        elevatorController = FindObjectOfType<ElevatorController>();
    }

    public void DownDown()
    {
        elevatorController.ButtonDown();
    }
}