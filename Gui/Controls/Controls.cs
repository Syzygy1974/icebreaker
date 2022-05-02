using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : guiObject
{
    public Transform buttons;
    public Transform elevatorButtons;

    public override void LocalAwake()
    {
        buttons = FindTransform ("Buttons");
        elevatorButtons = FindTransform ("ElevatorButtons");
        elevatorButtons.gameObject.SetActive(false);
    }

    public void ButtonsOff ()
    {
        buttons.gameObject.SetActive(false);
    }

    public void ButtonsOn ()
    {
        buttons.gameObject.SetActive(true);
    }

    public void ElevatorButtonsOff ()
    {
        elevatorButtons.gameObject.SetActive(false);
    }

    public void ElevatorButtonsOn ()
    {
        elevatorButtons.gameObject.SetActive(true);
    }
    
}
