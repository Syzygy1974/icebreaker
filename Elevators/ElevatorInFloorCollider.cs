using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Informa cuando el Elevator llega o sale de un piso.
// Este collider cuelta del Proximity Elevator y geometricamente
// es igual. Solo que esta en el LAYER ELEVATOR para poder
// detectarlo (Mientras que el Proximity esta en el mismo layer
// que el personaje).
public class ElevatorInFloorCollider : MonoBehaviour
{
    private ProximityElevator proximityElevator;

    void Awake () {
        proximityElevator = GetComponentInParent<ProximityElevator>();
    }
    
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Elevator Floor") {
            proximityElevator.CollisionDetected();
        }
    }

    void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Elevator Floor") {
            proximityElevator.CollisionExit();
        }
    }
}
