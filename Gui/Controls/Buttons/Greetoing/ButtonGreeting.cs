using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGreeting : guiObject
    {

    private PlayerController2D playerController;
    private GameObject avatar;

    // Start is called before the first frame update
    void Start()
    {
        //avatar = GameObject.Find("avatar_nk");
        playerController = FindObjectOfType<PlayerController2D>();
        Debug.Log("asigna avatar");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
        Debug.Log("Test");
        playerController.Greeting();
    }
}
