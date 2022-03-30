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

    StaircasesData staircasesData;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController2D>();
        description =  GetComponentInChildren<Text>();
    }

    public void RemoveObject() {
        loadedObject = false;
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
        }
        else {
            description.text = staircasesData.guiName;
        }
        
    }

    // =========================================================================================
    // Cuando se preciona "USE".
    // =========================================================================================
    // Envia la informacion del objeto a PlayerControles2D.
    public void UseDown()
    {
        // UseDown tiene que reconocer que tipo de objeto esta cargado para poder informar a Player !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        if (loadedObject) {
            // Solo envia el mensaje a UseStaircases en caso que la escalera este activa (Player dentro del proximity).
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
