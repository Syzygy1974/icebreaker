using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character_Staircases {

public class StaircasesManager {
    private bool collisionActive = false;
    private bool onStaircases = false;
    private bool onStaircasesPrev = false;
    private bool priorityActivation = false;
    private bool priorityDeactivation = false;
    public bool cover = true;
    private StaircasesData data;


    public void CollisionActive (bool active) {
        collisionActive = active;
    }

    // Se llama desde PlayerControler cuando recibe la informacion de "Use".
    // stairCasesData contiene la informacion sobre la escalera.
    public void useButton(StaircasesData stairCasesData) {
        if (stairCasesData == null) { return;}
        data = stairCasesData;
        StaircasesColission(data, true);
        cover = false;
    }

    // Se llama cuando el Player esta pisando una escalera.
    public void groundStaircases() {
        if (collisionActive) {
            priorityActivation = true;
            onStaircasesPrev = onStaircases;
            onStaircases = true;
            if (collisionActive) {
                StaircasesColission(data, true);
            }
        }
    }

    // Cuando NO pisa una escalera.
    public void groundNoStaircases() {
        if (collisionActive) {
            if (!onStaircasesPrev) { return; }
            onStaircasesPrev = onStaircases;
            onStaircases = false;
            // Si ahora no esta pisando escalera y en el paso anterior SI lo hacia: Desactiva la colision con las escalaras.
            if (onStaircasesPrev == true && onStaircases == false){
                PlayerController2D.isWalkingLeft = false;
                PlayerController2D.isWalkingRight = false;
                cover = true;
                StaircasesColission(data, false);
            }
            onStaircasesPrev = false;
            onStaircases = false;
            onStaircasesPrev = false;
            priorityActivation = false;
            priorityDeactivation = false;    
        }
    }

    // Activa y Desactiva la colision con el layer de las escaleras y con el cover.
    // Cuando se usa la escalera: Activa la colision contra el layer "Staircases" y desactiva la clision con el "cover",
    // y a la inversa cuando no se esta utilizando la escalera (desactiva colision con el layer y activa el cover).
    public void StaircasesColission(StaircasesData data, bool collisionStatus) {
        if (collisionStatus) {
            Physics2D.IgnoreLayerCollision( 13, 0, false);
            Physics2D.IgnoreCollision(PlayerController2D.controllerCollider, data.cover, true);
            CollisionActive(true);
        }
        else {
            Physics2D.IgnoreLayerCollision( 13, 0, true);
            Physics2D.IgnoreCollision(PlayerController2D.controllerCollider, data.cover, false);
            CollisionActive(false);
        }
    }
}

}

