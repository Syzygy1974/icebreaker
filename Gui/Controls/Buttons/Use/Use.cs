using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Use : guiObject
{
    private bool loadedObject = false;
    public GameObject Player;
    private PlayerController2D playerController;
    bool doorLocked;

    public Text description;

    string objectType ;
    StaircasesData staircasesData;
    ElevatorData elevatorData;
    DoorData doorData;

    float interval = 0.5f;
    int tap;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController2D>();
        description =  GetComponentInChildren<Text>();
    }

    public void RemoveObject() {
        loadedObject = false;
    }

    // =========================================================================================
    // GetMessage para: Door.
    // =========================================================================================
    // Recive la informacion del objeto del collider correspondiente.
    public void GetMessageDoor(DoorData data) {
        doorData = data;
        loadedObject = true;

        if (!doorData.active) {
            description.text = "Usar";
            objectType = "";
        }
        else {
            description.text = "Abrir";
            objectType = "Door";
        }
    }

    // =========================================================================================
    // GetMessage para: Elevator.
    // =========================================================================================
    // Recive la informacion del objeto del collider correspondiente.
    public void GetMessageElevator(ElevatorData data) {
        // Debug.Log ("LLEGA HASTA ACA..." +  data.guiName);
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

        // if (objectType == null) {

            
        // tap++;
 
        // if (tap == 1)
        // {
        //     StartCoroutine(DoubleTapInterval());
        // }
 
        // else if (tap > 1)
        // {
        //     // Do stuff
           
        //     // clean tap calculation
        //     tap = 0;
        // }

        // }

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
        else if (objectType == "Door") {
            if (doorData.active) {
                // Si la puerta esta requiere llava, envia el mensaje al Player para
                // chequear si posee la misma.
                // Si no requiere llave, envia el mensaje a la puerta directamente para abrirla.
                // (No informa al character en este caso).
                if (doorData.locked) {
                    bool a = playerController.LockedDoor(doorData.number);
                    if (a) {
                        doorData.door.SendMessage("Open");    
                    }
                } else {
                    doorData.door.SendMessage("Open");
                    // playerController.UseStaircases(doorData);
                }
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

    // Enumerator DoubleTapInterval()
    // {
    //     yield return new WaitForSeconds(interval);
    //     this.tap = 0;
    // }
}
