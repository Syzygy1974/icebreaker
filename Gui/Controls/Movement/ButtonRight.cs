using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRight : guiObject
{

    private PlayerController2D playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RightDown()
    {
        playerController.WalkingRightDown();
    }
  
    public void RightUp()
    {
        playerController.WalkingRightUp();
    }

}