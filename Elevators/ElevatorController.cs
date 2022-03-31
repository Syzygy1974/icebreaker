using System.Collections;
using System.Collections.Generic;
// using System.Threading.Tasks;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private Rigidbody2D controllerRigidbody;
    Vector2 movementInput;

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
        controllerRigidbody = GetComponentInParent<Rigidbody2D>();
        Debug.Log ("GET COMPONENT! " + controllerRigidbody);

        // Up();
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

        // // Una vez aplicada de aceleracion de movementInput, lo dejo en 0.
        // movementInput = Vector2.zero;

        // // Asigna la velocidad al RigidBody.
        controllerRigidbody.velocity = velocity;

    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Floor") {
            // useButton.SendMessage( "GetMessageStaircases", data);
            Debug.Log ("TOCO EL PISO.");
            // Stop();
        }

    }

    // void OnTriggerExit2D(Collider2D collision) {
    //     if (collision.gameObject.tag == "Floor") {
    //         Debug.Log ("YA NO TOCA EL PISO");
    //         // StaircasesData data = new StaircasesData();
    //         // data.active = false;
    //         // useButton.SendMessage( "GetMessageStaircases", data);
    //     }
    // }

}
