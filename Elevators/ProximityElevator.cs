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

// Determina si el Player esta en el rango de uso del Elevator (incluido el piso donde se encuentra).
// Tambien determina si el Elevator esta en ese piso o no, de manera que USE pueda presentar
// la ocion de CALL o de TAKE segun corresponda.

public class ProximityElevator : MonoBehaviour
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


    // ======================== Registra las colisiones del raycast =======================
    // Ambas funcioens son llamas por FixedUpdate cuando detecata o deja de detectar
    // la colision con el Elevator, mediante el RayCast.
    // ====================================================================================
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


    // ================ Detecta a Player en el rango de uso =========
    // Detecta cuando el Player entra y sale del rango de 
    // utilizacion del Elevator.
    // ==============================================================
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInProximityZone = true;
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

    // ================ Chequea colision con Elevator ===============
    // Permite que transmita la informacion a USE sobre el Elevator
    // ademas del Player: Permite decidir si se habilida la opcion
    // de CALL o de TAKE.
    // ==============================================================
    void FixedUpdate(){
        // RayCast que intercepte Elevator para actualizar funcion de colision.
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        float distance = 0.5f;

        Vector3 rayPosition = new Vector3 (controllerCollider.bounds.center.x, (controllerCollider.bounds.center.y - controllerCollider.bounds.extents.y), 0);
        RaycastHit2D hit = Physics2D.Raycast( rayPosition, direction, distance, ElevatorMask);
        // Debug.DrawRay(rayPosition, direction * distance, Color.green);
        if (hit.collider != null) {
            CollisionDetected();
            collide = true;
        }
        else {
            if (collide) {
                collide = false;
                CollisionExit();
            }
        }
    }
}
