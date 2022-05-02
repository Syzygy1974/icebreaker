using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDoor : MonoBehaviour
{
    // public GameObject useButton;

    private DoorData data = new DoorData();
    private GameObject useButton;

    void Awake()
    {
        data = gameObject.GetComponentInParent<DoorController>().data;
        useButton = gameObject.GetComponentInParent<DoorController>().useButton;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Informa a USE.
        // if (!data.open) {
            data.active = true;
            useButton.SendMessage ("GetMessageDoor", data);
        // }
    }

    void OnTriggerExit2D(Collider2D collision) {
        data.active = false;
        useButton.SendMessage ("GetMessageDoor", data);
    }
}
