using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : NPC
{
    // ================= INICIALIZA CARACTERISTICAS DEL NPC ====================
    // =========================================================================
    // public override void WhoIAm(){
    //     type = "citizen";
    //     level = 0;
    // }

    public void ReceiveGreeting(GameObject player)
    {
        Debug.Log ("LLEGA EL MENSAJE:" + player);
        // player.SendMessage ("NPCId", );
    }

}
