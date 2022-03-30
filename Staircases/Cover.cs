using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    Collider2D m_Collider;
    // Start is called before the first frame update
    void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    void EnableCover()
    {
        Debug.Log ("ACTIVA");
        m_Collider.enabled = true;
    }

    void DisableCover()
    {
        Debug.Log ("DESACTIVA");
        m_Collider.enabled = false;
    }
}

