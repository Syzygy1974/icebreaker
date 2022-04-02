using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    bool onFloor;

    public void GetMessageFloorCollider() {
        Debug.Log ("FRENA...");
        Stop();
        onFloor = true;
    }

    private Rigidbody2D controllerRigidbody;
    Vector2 movementInput;

    public void Call() {
        Debug.Log ("LLAMADA A ASENSOR.");
        Down();
    }

    public void Up() {
        movementInput = new Vector2 (0, 3);
    }

    public void Down() {
        movementInput = new Vector2 (0, -5);
    }

    public void Stop() {
        movementInput = new Vector2 (0, 0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        controllerRigidbody = GetComponent<Rigidbody2D>();
        Debug.Log ("GET COMPONENT! " + controllerRigidbody);

        // Down();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        // Debug.Log ("RIGIDBODY: " + controllerRigidbody);
        Vector2 velocity = controllerRigidbody.velocity;

        // // Calcula aceleracion.
        velocity += movementInput * 5 * Time.fixedDeltaTime;
        velocity.y = movementInput.normalized.y * 5f;

        // // Una vez aplicada de aceleracion de movementInput, lo dejo en 0.
        // movementInput = Vector2.zero;

        // // Asigna la velocidad al RigidBody.
        controllerRigidbody.velocity = velocity;

    }

}
