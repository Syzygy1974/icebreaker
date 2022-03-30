using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLeft : MonoBehaviour
{

    private PlayerController2D playerController;

    // Start is called before the first frame update
    void Start() // ESTO NO DEBERIA IR EN AWAKE ??????
    {
        playerController = FindObjectOfType<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeftDown()
    {
        playerController.WalkingLeftDown();
    }
  
    public void LefttUp()
    {
        playerController.WalkingLeftUp();
    }

}