using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public GameObject GUI;
    // private bool onFloor;
    private GameObject player;
    public static GameObject gui;
    ElevatorCalls calls = new ElevatorCalls();
    private Rigidbody2D controllerRigidbody;
    Vector2 movementInput;
    public int maxFloor;
    public int minFloor;
    public float elevatorVelocity;

    // ======================== RECEIVER: FLOOR COLIDER ========================
    // Recive del FLOOR COLLIDER la el piso en el que se encuentra el Elevator
    // y si se detecto al entrar (2) o salir (1) del collider.
    // =========================================================================
    // Si la direccion es UP y el handler del collider es ON ENTER o 
    // la direccion es DOWN y se trata de un ON EXIT, entonces el Elevator esta
    // pasando por un piso. La funcion CurrentFloor determinara si se sigue moviendo
    // o se detiene en un piso destino.
    public void GetMessageFloorCollider(int[] floorData) {
        int floor = floorData[0];
        int colliderType = floorData[1];

        if (calls.Direction() == 2 && colliderType == 2) {
            calls.CurrentFloor(floor);
            calls.RemoveCall(floor);
        }
        else if (calls.Direction() == 1 && colliderType == 1) {
            calls.CurrentFloor(floor);
            calls.RemoveCall(floor);
        }
    }

    // ======================== RECEIVER: PLAYER / NPC =========================
    // Recive una llamada al Elevator y la agrega en el pool de llamadas.
    // Si cuando se realiza la llamada, el personaje esta en el mismo piso que
    // Elevator, informa al Player de esto (ElevatorInTheFloor).
    // =========================================================================
    public void Call (ElevatorData elevatorData) {
        player = elevatorData.player;
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
        movementInput = new Vector2 (0, elevatorVelocity);
    }

    public void Down() {
        movementInput = new Vector2 (0, -elevatorVelocity);
    }

    public void Stop() {
        movementInput = new Vector2 (0, 0);
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
            return upOrDown;
        }

        public void CurrentFloor (int floor) {
        currentFloor = floor;
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
            if (PendingCalls() > 0) {
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
        controllerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
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
}
