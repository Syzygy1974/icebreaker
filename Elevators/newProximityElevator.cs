using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ElevatorData {
//         public string name = "Elevator";
//         public string guiName = "Ascensor";
//         public bool active = true;
//         public bool inFloor = false;
//         public string upOrDown = "";
//         public int currentFloor = 0;
//         public GameObject elevator;
//         public GameObject player;
//         public Vector2 proximityCenter;
//         public int floor;
//     }

public class newProximityElevator : MonoBehaviour
{
    public GameObject useButton;
    public GameObject elevator;
    public int floor;
    Collider2D elevatorCollider;

    Vector2 proximityCenter;
    ElevatorData data = new ElevatorData();
    bool playerInProximityZone;
    bool elevatorInTheFloor;

    private LayerMask ElevatorMask;
    private Collider2D controllerCollider;
    private bool collide = false;


// ======================== RECEIVER: ElevatorInFloorCollider ===========================
// Es llamado cuando se el Elevator llega al piso del collider.
// ======================================================================================
    public void CollisionDetected()
    {
        elevatorInTheFloor = true;
        // Debug.Log ("playerInProximityZone: " + playerInProximityZone);
        if (playerInProximityZone) {
            // Debug.Log ("LLEGA HASTA ACA...");
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
            // Debug.Log ("playerInProximityZone: TRUE");
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
        ElevatorMask = LayerMask.GetMask("Elevators");
        controllerCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate(){

        // if (controllerCollider.IsTouchingLayers(ElevatorMask)) {
        //     Debug.Log ("******************** TOCO AL ASCENSOR. ****************************");
        // }

        // RayCast que intercepte Elevator para actualizar funcion de colision.
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        float distance = 0.1f;

        Vector3 rayPosition = new Vector3 (controllerCollider.bounds.center.x, (controllerCollider.bounds.center.y - controllerCollider.bounds.extents.y), 0);
        RaycastHit2D hit = Physics2D.Raycast( rayPosition, direction, distance, ElevatorMask);
        Debug.DrawRay(rayPosition, direction * distance, Color.green);
        if (hit.collider != null) {
            CollisionDetected();
            collide = true;
            // Debug.Log ("******************** TOCO AL ASCENSOR. ****************************");
        }
        else {
            if (collide) {
                collide = false;
                CollisionExit();
            }
        }
    }
}
