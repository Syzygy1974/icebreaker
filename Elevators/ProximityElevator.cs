using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorData {
        public string name = "Elevator";
        public string guiName = "Ascensor";
        public bool active = true;
        public bool inFloor = false;
        public string upOrDown = "";
        public int currentFloor = 0;
        public GameObject elevator;
        public GameObject player;
        public Vector2 proximityCenter;
        public int floor;
    }

public class ProximityElevator : MonoBehaviour
{
    public ElevatorController elevatorController;
    public GameObject useButton;
    public GameObject elevator;
    Collider2D elevatorCollider;
    public int floor;
    Vector2 proximityCenter;
    // int elevatorFloor;
    ElevatorData data = new ElevatorData();
    bool playerInProximityZone;
    bool elevatorInTheFloor;

// ======================== RECEIVER: ElevatorInFloorCollider ===========================
// Es llamado cuando se el Elevator llega a
// ======================================================================================
    public void CollisionDetected()
    {
        elevatorInTheFloor = true;
        if (playerInProximityZone) {
            data.inFloor = true;
            data.guiName = "Subir";
            useButton.SendMessage( "GetMessageElevator", data);
        }
    }

    public void CollisionExit()
    {
        elevatorInTheFloor = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInProximityZone = true;
            // data.upOrDown = "Down";
            data.elevator = elevator;
            data.floor = floor;
            data.active = true;
            data.inFloor = false;
            data.proximityCenter = proximityCenter;
            if (elevatorInTheFloor) {
                data.inFloor = true;
                data.guiName = "Subir";
                useButton.SendMessage( "GetMessageElevator", data);
            } else {
                data.guiName = "Llamar";
                useButton.SendMessage( "GetMessageElevator", data);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInProximityZone = false;
            data.active = false;
                useButton.SendMessage( "GetMessageElevator", data);
        }
    }

    void Awake(){
        elevatorCollider = GetComponent<Collider2D>();
        proximityCenter = elevatorCollider.bounds.center;
    }
}
