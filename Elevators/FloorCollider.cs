using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    public GameObject elevator;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Elevator") {
            elevator.SendMessage("GetMessageFloorCollider");
            Debug.Log ("TOCO EL PISO.");
        }
    }
}
