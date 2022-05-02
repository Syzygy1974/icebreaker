using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : guiObject
{
     void GetMessage(string objectAndCommand)
    {
        // Parsea el parametro recivido: objeto:commando (Ej.: Buttons:Off para desactivar los botones.)
        string guiObject;
        string command;
        string[] objectAndCommandParsed = objectAndCommand.Split(':');
        guiObject = objectAndCommandParsed[0];
        command = objectAndCommandParsed[1];

        // Envia el mensaje de "command" al objeto de "guiObject".
        Transform obj = FindTransform (guiObject);
        obj.SendMessage (command);
                                        Debug.Log ("LLEGA HASTA ACA............5");
    }
}
