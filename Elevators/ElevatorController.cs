using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public GameObject useButton;
    public GameObject GUI;
    public int floor;

    private int touch = 0;
    [HideInInspector]  public GameObject elevator;
    private GameObject player;        
    private ElevatorCalls calls = new ElevatorCalls();
    private Rigidbody2D controllerRigidbody;
    private Collider2D controllerCollider;
    private Vector2 movementInput;
    public int maxFloor;
    public int minFloor;
    public float elevatorVelocity;
    private LayerMask floorGroundMask;
    private RigidbodyConstraints2D originalConstraints;
    private Floor floorScript;

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

    // ======================== Funciones de movimiento  =======================
    // =========================================================================
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

    // ================= Clase que maneja las llamadas al Elevator =============
    // Lista de llamadas, eliminacion de llamadas por donde ya paso el Elevator y
    // determina la direccion que debera seguir segun la lista.
    // =========================================================================
    private class ElevatorCalls : MonoBehaviour {

        List<int> elevatorCalls = new List<int>();
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
        Debug.Log(System.Environment.StackTrace);
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
        calls.CurrentFloor(floor);
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

    // ======================== UPDATEFLOOR =========================
    // Determina cuando Elevator pasa por un piso. Reemplaza al
    // anterior Floor Collider.
    // ==============================================================
    void UpdateFloor() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        // RayCast desde el centro (x) y en la parte inferior de del Elevator (y).
        Vector3 rayPosition = new Vector3 (controllerCollider.bounds.center.x, (controllerCollider.bounds.center.y - controllerCollider.bounds.extents.y), 0);
        RaycastHit2D hit = Physics2D.Raycast( rayPosition, direction, distance, floorGroundMask);
        // Debug.DrawRay(rayPosition, direction * distance, Color.red);

        // Si el RayCast toca el floor: Envia el numero de piso a GetMessageCollider.
        // Esto detecta el piso cuando el Elevator esta bajando.
        if (hit.collider != null) {
            if (touch != 1){
                floorScript = hit.collider.gameObject.GetComponent<Floor>();
                touch = 1;
                if (calls.Direction() == 2) {
                    // Informa en que piso esta y lo elimina de la lista de pisos a recorrer.
                    calls.CurrentFloor(floorScript.FloorNumber());
                    calls.RemoveCall(floorScript.FloorNumber());
                }
            }
        }
        // Si existio una colision y liego termina, actualiza el piso cuando
        // el Elevator esta subiendo. Hacerlo de esta forma evita que el 
        // Elevator se detenga enterrado en el piso (donde se daria la primera
        // colision)
        else {
            if (touch == 1) {
                touch = 0;
                if (calls.Direction() == 1) {
                    calls.CurrentFloor(floorScript.FloorNumber());
                    calls.RemoveCall(floorScript.FloorNumber());
                }
            }
            touch = 0;
        }
    }
}
