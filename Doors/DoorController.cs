using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData {
    public bool locked;
    public bool open;
    public int number;
    public bool active = true;
    public string name = "Door";
    public string guiName = "Puerta";
    public GameObject door;
    }

public class DoorController : MonoBehaviour
{
    public bool locked;
    private string doorState;  // "Closed" o "open"
    public int number;
    public DoorData data;
    private Collider2D doorCollider, ProximityCollider;
    public GameObject useButton;
    public float timeOut;
    private float timeRemaining; // = 10;

    void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        ProximityCollider = GetComponentInChildren<Collider2D>();
        data = new DoorData();
        Debug.Log ("LOCKED: " + data.locked);
        data.open = false;
        data.locked = locked;
        data.door = gameObject;
        timeRemaining = timeOut;
    }

    void Update(){

        // Si la puerta esta abierta, pone la cierra al finalizar el tiempo de apertura.
        if (data.open) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else {
                Close();
            }
        }
    }

    public void Open() {
        data.open = true;
        timeRemaining = timeOut;
        doorCollider.enabled = false;
    }

    public void Close() {
        data.open = false;
        doorCollider.enabled = true;
        useButton.SendMessage ("GetMessageDoor", data);
        // ProximityCollider.enabled = false;
        // ProximityCollider.enabled = true;
        
    }
}
