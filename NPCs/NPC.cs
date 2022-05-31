using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoIAm
{
    string type = "citizen";
    int level = 0;

        string Type(){
            return type;
        }
}

public class NPC : MonoBehaviour
{
    WhoIAm whoIAm;
    // whoIAm.Type();
    whoIAm = new WhoIAm();

    // Tipos posibles de NPC: CITIZEN - BUREAUCRAT - POLITICIAN - PERONCHO
    // public string type = "citizen";
    // Nivel de dificultad del NPS: 0 - 5.
    // public int level = 0;

    // Start is called before the first frame update
    void Awake()
    {
        // WhoIAm();
    }
}
