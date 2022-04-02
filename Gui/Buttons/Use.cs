using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    private bool loadedObject = false;
    public GameObject Player;
    private PlayerController2D playerController;

    public Text description;

    string objectType ;
    StaircasesData staircasesData;
    ElevatorData elevatorData;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController2D>();
        description =  GetComponentInChildren<Text>();
    }

    public void RemoveObject() {
        loadedObject = false;
    }


    // =========================================================================================
    // GetMessage para: Elevator.
    // =========================================================================================
    // Recive la informacion del objeto del collider correspondiente.
    public void GetMessageElevator(ElevatorData data) {
        elevatorData = data;
        loadedObject = true;
        if (!elevatorData.active) {
            description.text = "Usar";
            objectType = "";
        }
        else {
            description.text = elevatorData.guiName;
            objectType = "Elevator";
        }
    }

    // =========================================================================================
    // GetMessage para: Staircases.
    // =========================================================================================
    // Recive la informacion del objeto del collider correspondiente.
    public void GetMessageStaircases( StaircasesData data ) {
        staircasesData = data;
        loadedObject = true;
        if (!staircasesData.active) {
            description.text = "Usar";
            objectType = "";
        }
        else {
            description.text = staircasesData.guiName;
            objectType = "Staircases";
        }
    }

    // =========================================================================================
    // Cuando se presiona "USE".
    // =========================================================================================
    // Envia la informacion del objeto a PlayerControles2D.
    public void UseDown()
    {
        if (objectType == "Elevator") {
                if (elevatorData.active) {
                    playerController.UseElevator(elevatorData);
                }
        }
        else if (objectType == "Staircases") {
            if (staircasesData.active) {
                playerController.UseStaircases(staircasesData);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log ("Direction: " + info.direction);
    }
}
