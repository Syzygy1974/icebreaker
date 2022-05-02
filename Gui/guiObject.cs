using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guiObject : MonoBehaviour
{
    public Transform[] children;
    void Awake()
    {
        // Obtiene el listado de childs que cuelgan de GUI.
        children = transform.GetComponentsInChildren<Transform>();

        LocalAwake ();
    }

    public virtual void LocalAwake (){}

    public void Off ()
    {
        gameObject.SetActive(false);
        Debug.Log ("APAGAR BOTONES.");
    }

    public void On ()
    {
        gameObject.SetActive(true);
        Debug.Log ("APAGAR BOTONES.");
    }

    public Transform FindTransform (string name)
    {
        // Busca el objeto que coincida con el indicado en name y lo regresa.
        foreach (Transform obj in children)
        {
            if (obj.name == name) {
                return obj;
            }
        }
        return null;
    }
}
