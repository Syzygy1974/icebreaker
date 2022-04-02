using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorData {
        public string name = "Elevator";
        public string guiName = "Asensor";
        public bool active = true;
        public string upOrDown = "";
        public int currentFloor = 0;
        public GameObject elevator;
    }

public class ProximityElevator : MonoBehaviour
{
    public GameObject useButton;
    public GameObject elevator;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {

            ElevatorData data = new ElevatorData();
            data.upOrDown = "Down";
            data.elevator = elevator;

            useButton.SendMessage( "GetMessageElevator", data);
            Debug.Log ("Personaje en proximity de asensor.");
            // Stop();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            ElevatorData data = new ElevatorData();
            data.active = false;
            useButton.SendMessage( "GetMessageElevator", data);
        }
    }

}
