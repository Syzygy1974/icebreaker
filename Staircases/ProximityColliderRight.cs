using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Informacion sobre este extremo de la escalera: 
//                                                  name: nombre del objeto.
//                                                  active: true para activo, false para inactivo.
//                                                  direction: Indica hacia donde avanzar para utilizar la escalera.
//                                                  upOrDown: "Up" si va hacia arriba o "Down" si baja.
//                                                  currentFloor: Indica el piso actual (en el que esta este extremo)
//                                                  initialPosition: Posicion en la que se ubicara el Player para avanzar (en el sentido indicad1o en "direction").
// public class StaircasesData {
//     public string name = "Staircases";
//     public bool active = true;
//     public string direction = "Right";
//     public string upOrDown = "Down";
//     public int currentFloor = 1;
//     public Vector2 initialPosition;
// }

public class ProximityColliderRight : MonoBehaviour
{
    public GameObject useButton;
    public GameObject position;
    public GameObject myCover;

    void OnTriggerEnter2D(Collider2D collision) {

    StaircasesData data = new StaircasesData();
        data.active = true;
        data.direction = "Left";
        data.upOrDown = "Down";
        data.currentFloor = 0;
        data.initialPosition = position.transform.position;
        data.cover = myCover.gameObject.GetComponent<Collider2D>();

        // openWith.Add("txt", data );
        // Debug.Log (data.openWith["txt"]);

        if (collision.gameObject.tag == "Player") {
            useButton.SendMessage( "GetMessageStaircases", data);
        }

    }

    void OnTriggerExit2D(Collider2D collision) {
        // if (collision.gameObject.tag == "Player") {
        //     useButton.SendMessage( "RemoveObject");
        // }
        if (collision.gameObject.tag == "Player") {
            StaircasesData data = new StaircasesData();
            data.active = false;
            useButton.SendMessage( "GetMessageStaircases", data);
        }
    }

}
