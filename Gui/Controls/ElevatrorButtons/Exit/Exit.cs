using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : guiObject
{
    private PlayerController2D playerController;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController2D>();
    }
    public void ElevatorExit()
    {
        playerController.SendMessage("ElevatorExit");
    }
}
