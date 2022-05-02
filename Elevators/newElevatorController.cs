using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newElevatorController : MonoBehaviour
{
    private int touch = 0;
    public GameObject useButton;
    public int floor;
    [HideInInspector]  public GameObject elevator;
    public GameObject GUI;
    private GameObject player;        
    ElevatorCalls calls = new ElevatorCalls();
    private Rigidbody2D controllerRigidbody;
    private Collider2D controllerCollider;
    Vector2 movementInput;
    public int maxFloor;
    public int minFloor;
    public float elevatorVelocity;
    private LayerMask floorGroundMask;

    private RigidbodyConstraints2D originalConstraints;
    Floor floorScript;

    // ======================== RECEIVER: FLOOR COLIDER ========================
    // Recive del FLOOR COLLIDER la el piso en el que se encuentra el Elevator
    // y si se detecto al entrar (2) o salir (1) del collider.
    // =========================================================================
    // Si la direccion es UP y el handler del collider es ON ENTER o         
    // la direccion es DOWN y se trata de un ON EXIT, entonces el Elevator esta
    // pasando por un piso. La funcion CurrentFloor determinara si se sigue moviendo
    // o se detiene en un piso destino.
    public void GetMessageFloorCollider2(int floor) {
            calls.CurrentFloor(floor);
            Debug.Log ("PISO: " + floor + " DIRECCION: " + calls.Direction());
            Debug.Log ("PISO: " + calls.GetCurrentFloor());
            calls.RemoveCall(floor);
    }

    // ======================== RECEIVER: PLAYER / NPC =========================
    // Recive una llamada al Elevator y la agrega en el pool de llamadas.
    // Si cuando se realiza la llamada, el personaje esta en el mismo piso que
    // Elevator, informa al Player de esto (ElevatorInTheFloor).
    // =========================================================================
    public void Call (ElevatorData elevatorData) {
        player = elevatorData.player;
        // Debug.Log ("LLAMADA DEL PISO: " + elevatorData.floor);
        if (calls.GetCurrentFloor() == elevatorData.floor) {
            player.SendMessage("ElevatorInTheFloor");
        }
        calls.Add (elevatorData.floor);
    }

    // ======================== RECEIVER: ELEVETOR GUI =========================
    // Recive una llamada al Elevator y la agrega en el pool de llamadas.
    // Up or Down.
    // =========================================================================
    public void ButtonDown() {
        if ((calls.GetCurrentFloor()-1) >= minFloor) {
            calls.Add (calls.GetCurrentFloor()-1);
            movementInput = new Vector2 (0, -elevatorVelocity);
        }
    }

    public void ButtonUp() {
        if ((calls.GetCurrentFloor()+1) <= maxFloor) {
            calls.Add (calls.GetCurrentFloor()+1);
            movementInput = new Vector2 (0, -elevatorVelocity);
        }
    }
    // ======================== RECEIVER: CHARACTER  ===========================
    // Recive el aviso del player indicando que ya esta dentro del Elevator.
    // Activa los botondes de la GUI del ELEVATOR.
    // =========================================================================
    public void PlayerInElevator() {
        GUI.SendMessage ("GetMessage", "Controls:ElevatorButtonsOn");
    }

    public void Up() {
        controllerRigidbody.constraints = originalConstraints;
        movementInput = new Vector2 (0, elevatorVelocity);
    }

    public void Down() {
        controllerRigidbody.constraints = originalConstraints;
        movementInput = new Vector2 (0, -elevatorVelocity);
    }

    public void Stop() {
        movementInput = new Vector2 (0, 0);
        controllerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        // Debug.Log ("METO FEREEZ");
    }

    private class ElevatorCalls : MonoBehaviour {

        List<int> elevatorCalls = new List<int>();
        bool moving = false;
        int currentFloor;
        int upOrDown; //0 Stoped, 1 Up, 2 Down.

        public bool IsStoped (int floor){
            if (upOrDown == 0) { return true; }
            else { return false; }
        }

        public bool IsOnTheList (int floor){
            return (elevatorCalls.Contains(floor));
        }

        public void RemoveCall (int floor) {
           elevatorCalls.Remove(floor);
        }

        public int Direction () {
            // Debug.Log ("DIRECTION: " + upOrDown);
            return upOrDown;
        }

        public void CurrentFloor (int floor) {
        Debug.Log(System.Environment.StackTrace);
        currentFloor = floor;
        Debug.Log ("SETEA CURRENT FLOOR: " + currentFloor);
        UpOrDown();
        }

        public int GetCurrentFloor () {
            return currentFloor;
        }

        public void Add (int floor) {
            if (!elevatorCalls.Contains (floor) && currentFloor != floor) elevatorCalls.Add (floor);
            UpOrDown();
        }

        public int PendingCalls () {
            return elevatorCalls.Count;
        }

        public int NextFloor () {
            return elevatorCalls[0];
        }

        public void UpOrDown () {
            Debug.Log ("CURRENT FLOOR: " + currentFloor);

           if (PendingCalls() > 0) {

                Debug.Log ("NETX FLOOR: " + NextFloor());

                if (currentFloor > NextFloor()) {
                    upOrDown = 2;
                }
                else if (currentFloor < NextFloor()) { 
                    upOrDown = 1;
                }
                else {
                    upOrDown = 0;
                }
            }
            else {
                upOrDown = 0;
            }
        }
    }

    // Determina la direccion en la que se mueve el Elevator y
    // envia un mensaje al character para informarle si se
    // esta moviendo o esta detenido (y en ese caso pueda
    // salir del Elevator).
    public void UpdateDirection () {
        if (player == null) return;
        if (calls.Direction() == 1) {
            player.SendMessage ("ElevatorStopped", false);
            Up();
        }
        else if (calls.Direction() == 2) {
            player.SendMessage ("ElevatorStopped", false);
            Down();
        }
        else {
            Stop();
            player.SendMessage ("ElevatorStopped", true);
            }
    }

    // Start is called before the first frame update
    void Awake()
    {
        calls.CurrentFloor(floor);
        Debug.Log ("CURRENT FLOOR AWAKE");
        floorGroundMask = LayerMask.GetMask("Floor");
        controllerRigidbody = GetComponent<Rigidbody2D>();
        originalConstraints = controllerRigidbody.constraints;
        controllerCollider = GetComponent<Collider2D>();
        elevator = gameObject;
    }

    void FixedUpdate()
    {
        UpdateFloor();
        UpdateDirection();
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        Vector2 velocity = controllerRigidbody.velocity;

        // Calcula aceleracion.
        velocity += movementInput * elevatorVelocity * Time.fixedDeltaTime;
        velocity.y = movementInput.normalized.y * elevatorVelocity;

        // Una vez aplicada de aceleracion de movementInput, lo dejo en 0.
        movementInput = Vector2.zero;

        // Asigna la velocidad al RigidBody.
        controllerRigidbody.velocity = velocity;
    }

    void UpdateFloor() {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;
        // Debug.Log ("controllerRigidbody.position: " + controllerRigidbody.position);
        // Debug.Log ("controllerCollider.bounds.center" + controllerCollider.bounds.center);
        // Debug.Log ("controllerCollider.bounds.extents" + controllerCollider.bounds.extents);
        Vector3 rayPosition = new Vector3 (controllerCollider.bounds.center.x, (controllerCollider.bounds.center.y - controllerCollider.bounds.extents.y), 0);
        RaycastHit2D hit = Physics2D.Raycast( rayPosition, direction, distance, floorGroundMask);
        // RaycastHit2D hit = Physics2D.Raycast(controllerRigidbody.position, direction, distance, floorGroundMask);

        Debug.DrawRay(rayPosition, direction * distance, Color.red);

        if (hit.collider != null) {
            if (touch != 1){ 
                floorScript = hit.collider.gameObject.GetComponent<Floor>();
                touch = 1;
                if (calls.Direction() == 2) {
                    GetMessageFloorCollider2(floorScript.FloorNumber());
                    Debug.Log ("CURRENT FLOOR RAY COLLIDER 1: " + floorScript.FloorNumber());
                    // calls.CurrentFloor(floor);
                    // calls.RemoveCall(floor);
                }
            }
        }
        else {
            if (touch == 1) {
                touch = 0;
                if (calls.Direction() == 1) {
                    GetMessageFloorCollider2(floorScript.FloorNumber());
                    // calls.CurrentFloor(floor);
                    // Debug.Log ("CURRENT FLOOR RAY COLLIDER 0: " + floor);
                    // calls.RemoveCall(floor);
                }
            }
            touch = 0;
        }
    }
}
