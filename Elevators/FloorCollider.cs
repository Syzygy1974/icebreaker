using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    private GameObject elevator;
    private int floor;

    void Awake(){
        floor = gameObject.GetComponentInParent<ProximityElevator>().floor;
        elevator = gameObject.GetComponentInParent<ProximityElevator>().elevator;
    }

    // Envia el Elevator un array con el piso en el que sucedio la colicion [0] y un
    // indice que indica si entra al collider (2) o sale del mismo (1) [1].
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Elevator Floor") {
            int[] floorData = new int[] { floor, 2};
            elevator.SendMessage("GetMessageFloorCollider", floorData);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Elevator Floor") {
            int[] floorData = new int[] { floor, 1};
            elevator.SendMessage("GetMessageFloorCollider", floorData);
        }
    }
}
